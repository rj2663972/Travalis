using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Domain.Core.PredicateBuilder
{
    public static class PredicateBuilderExtensions
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var finalExpression = Expression.Lambda<Func<T, bool>>(Expression.OrElse(
                       new SwapVisitor(expr1.Parameters[0], expr2.Parameters[0]).Visit(expr1.Body), expr2.Body), expr2.Parameters);

            return finalExpression;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var finalExpression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
            new SwapVisitor(expr1.Parameters[0], expr2.Parameters[0]).Visit(expr1.Body), expr2.Body), expr2.Parameters);
            return finalExpression;
        }

        public static Type GetTypeOfPropertyName<T>(string propertyName, bool collection = false) where T : BaseEntity
        {
            string[] bits = propertyName.Split('.');
            PropertyInfo propertyInfo = typeof(T).GetProperty(bits[0]);
            for (int i = 1; i < bits.Length; i++)
            {
                if (propertyInfo.PropertyType.GetGenericArguments().Count() > 0)
                {
                    if (collection) return propertyInfo.PropertyType;
                    propertyInfo = propertyInfo.PropertyType.GetGenericArguments()[0].GetProperty(bits[i]);
                }
                else
                {
                    propertyInfo = propertyInfo.PropertyType.GetProperty(bits[i]);
                }
            }
            return propertyInfo.PropertyType;
        }

        public static PropertyInfo GetTypeOfPropertyCollection<T>(string propertyName) where T : BaseEntity
        {
            string[] bits = propertyName.Split('.');
            PropertyInfo propertyInfo = typeof(T).GetProperty(bits[0]);
            for (int i = 1; i < bits.Length; i++)
            {
                if (propertyInfo.PropertyType.GetGenericArguments().Count() > 0)
                {
                    propertyInfo = propertyInfo.PropertyType.GetGenericArguments()[0].GetProperty(bits[i]);
                }
                else
                {
                    propertyInfo = propertyInfo.PropertyType.GetProperty(bits[i]);
                }
            }
            return propertyInfo;
        }

        public static bool IsNullableType(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Expression<Func<T, bool>> AddPredIf<T>(this Expression<Func<T, bool>> expr, object value, string propertyName, bool condition, bool? greaterOrLessThan = null) where T : BaseEntity
        {
            Expression<Func<T, bool>> predicate = True<T>();

            if (condition)
                predicate = expr.AddPred(value, propertyName, greaterOrLessThan);

            expr = expr.And(predicate);
            return expr;
        }


        public static Expression<Func<T, bool>> AddPred<T>(this Expression<Func<T, bool>> expr, object value, string propertyName, bool? greaterOrLessThan = null) where T : BaseEntity
        {
            Expression<Func<T, bool>> predicate = True<T>();

            try
            {
                Type typeOfValue = GetTypeOfPropertyName<T>(propertyName);
                Type typeOfCollection = GetTypeOfPropertyName<T>(propertyName, collection: true);

                if (HasToEvaluateValue(value, typeOfValue))
                {
                    if (IsIEnumerable(typeOfCollection) == false)
                    {
                        predicate = BuildExpressionForProperty<T>(value, propertyName, typeOfValue, greaterOrLessThan);
                    }
                    else
                    {
                        predicate = BuildExpressionForCollectionProperty<T>(value, propertyName, typeOfValue, typeOfCollection, greaterOrLessThan);
                    }
                    expr = expr.And(predicate);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return expr;
        }

        public static Expression<Func<T, bool>> BuildExpressionForProperty<T>(object value, string propertyName, Type typeOfValue, bool? greaterOrLessThan = null) where T : BaseEntity
        {
            Expression<Func<T, bool>> predicate;
            ParameterExpression paramExp = Expression.Parameter(typeof(T), "e");
            var propExp = CreateExpression(paramExp, propertyName);
            MethodInfo method = typeOfValue.GetMethod(GetMethodTypeOfValue(typeOfValue), new[] { typeOfValue });
            Expression expression;
            if (IsNullableType(typeOfValue) && greaterOrLessThan == null) typeOfValue = typeof(object);
            try
            {
                var someValue = Expression.Constant(value, typeOfValue);
                if (greaterOrLessThan == true)
                {
                    expression = Expression.GreaterThanOrEqual(propExp, someValue);
                }
                else if (greaterOrLessThan == false)
                {
                    expression = Expression.LessThanOrEqual(propExp, someValue);
                }
                else
                {
                    expression = Expression.Call(propExp, method, someValue);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            predicate = Expression.Lambda<Func<T, bool>>(expression, paramExp);
            return predicate;
        }


        private static Expression<Func<T, bool>> BuildExpressionForCollectionProperty<T>(object value, string propertyName, Type typeOfValue, Type typeOfCollection, bool? greaterOrLessThan = null) where T : BaseEntity
        {
            ParameterExpression paramExpPromotion = Expression.Parameter(typeof(T), "p");

            var propertyNameReport = string.Join(".", propertyName.Split(".").Skip(1).ToList());

            //(e) => e.Date Equals ""
            var lambdaForReport = GetExpressionForPropertyType(value, propertyNameReport, typeOfValue, typeOfCollection, greaterOrLessThan);

            // (p) => p.Reports.Any((e) => e.Date Equals ""))
            var collectionProperty = Expression.Property(paramExpPromotion, propertyName.Split(".")[0]);
            var resultExpression = CallAny(collectionProperty, lambdaForReport);
            return Expression.Lambda<Func<T, Boolean>>(resultExpression, paramExpPromotion);
        }

        private static Expression GetExpressionForPropertyType(object value, string propertyNameReport, Type typeOfValue, Type typeOfCollection, bool? greaterOrLessThan = null)
        {
            MethodInfo methodBuildExpression = typeof(PredicateBuilderExtensions).GetMethod("BuildExpressionForProperty");
            MethodInfo genericMethodBuildExpression = methodBuildExpression.MakeGenericMethod(typeOfCollection.GetGenericArguments()[0]);
            return genericMethodBuildExpression.Invoke(null,
                new object[] { value, propertyNameReport, typeOfValue, greaterOrLessThan }) as Expression;
        }

        private static Type GetPropertyTypeOfCollection(Type collectionType, string propertyNameReport)
        {
            MethodInfo methodPropertyName = typeof(PredicateBuilderExtensions).GetMethod("GetTypeOfPropertyName");
            MethodInfo genericMethodPropertyName = methodPropertyName.MakeGenericMethod(collectionType);
            return (genericMethodPropertyName.Invoke(null, new object[] { propertyNameReport }) as PropertyInfo).PropertyType;
        }

        private static Expression CallAny(Expression collection, Expression predicateExpression)
        {
            Type cType = GetIEnumerableImpl(collection.Type);

            Type elemType = cType.GetGenericArguments()[0];
            Type predType = typeof(Func<,>).MakeGenericType(elemType, typeof(bool));

            MethodInfo anyMethod = (MethodInfo)
                GetGenericMethod(typeof(Enumerable), "Any", new[] { elemType },
                    new[] { cType, predType }, BindingFlags.Static);

            return Expression.Call(
                anyMethod,
                collection,
                predicateExpression);
        }

        private static MethodBase GetGenericMethod(Type type, string name, Type[] typeArgs, Type[] argTypes, BindingFlags flags)
        {
            int typeArity = typeArgs.Length;
            var methods = type.GetMethods()
                .Where(m => m.Name == name)
                .Where(m => m.GetGenericArguments().Length == typeArity)
                .Select(m => m.MakeGenericMethod(typeArgs));

            return Type.DefaultBinder.SelectMethod(flags, methods.ToArray(), argTypes, null);
        }

        private static bool IsIEnumerable(Type type)
        {
            return type.IsGenericType
                && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                    type.GetInterface("IEnumerable") != null);
        }

        private static Type GetIEnumerableImpl(Type type)
        {
            // Get IEnumerable implementation. Either type is IEnumerable<T> for some T, 
            // or it implements IEnumerable<T> for some T. We need to find the interface.
            if (IsIEnumerable(type))
                return type;
            Type[] t = type.FindInterfaces((m, o) => IsIEnumerable(m), null);
            return t[0];
        }


        private static Expression CreateExpression(ParameterExpression paramExp, string propertyName)
        {
            Expression propExp = paramExp;
            foreach (var propName in propertyName.Split('.'))
            {
                propExp = Expression.Property(propExp, propName);
            }
            return propExp;
        }

        private static bool HasToEvaluateValue(object value, Type valueType)
        {
            if (value == null) return false;

            if (valueType == typeof(Guid))
            {
                Guid valueConverted = Guid.Parse(value.ToString());
                return Guid.Empty != valueConverted;
            }

            return true;
        }

        private static string GetMethodTypeOfValue(Type type)
        {
            if (type == typeof(string)) return "Contains";

            return "Equals";
        }
    }

    class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
