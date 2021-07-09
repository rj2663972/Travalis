using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.EntityConfigurations
{
    public static class EntityTypeConfigurations
    {
        public static void DefaultConfiguration(ModelBuilder builder)
        {
            builder.Model.GetEntityTypes()
               .SelectMany(m => m.GetProperties())
               .Where(m => m.ClrType == typeof(string))
               .ToList()
               .ForEach(prop => { prop.SetMaxLength(150); });

            builder.Model.GetEntityTypes()
                .Where(entityType => typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                .ToList()
                .ForEach(entityType =>
                {
                    builder.Entity(entityType.ClrType).Property<DateTime>(AuditableEntityFields.CreateOn);
                    builder.Entity(entityType.ClrType).Property<DateTime>(AuditableEntityFields.UpdateOn);
                });

            builder.Model.GetEntityTypes()
              .Where(p => typeof(IDeletableEntity).IsAssignableFrom(p.ClrType))
              .ToList()
               .ForEach(entityType =>
               {
                   builder.Entity(entityType.ClrType)
                   .HasQueryFilter(ConvertFilterExpression<IDeletableEntity>(e => !e.IsDeleted, entityType.ClrType));
               });
        }

        private static LambdaExpression ConvertFilterExpression<TInterface>(
               Expression<Func<TInterface, bool>> filterExpression,
               Type entityType)
        {
            var newParam = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

            return Expression.Lambda(newBody, newParam);
        }
    }
}
