using Data.DataContext;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Specifications;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected TravalisDataContext _dbContext { get; set; }

        public Repository(TravalisDataContext travalisDataContext)
        {
            _dbContext = travalisDataContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().SingleOrDefault(m => m.Id == id);
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<T> GetSingleBySpecAsync(ISpecification<T> spec)
        {
            return await GetQueryableBySpec(spec).SingleOrDefaultAsync();
        }

        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return GetQueryableBySpec(spec).SingleOrDefault();
        }

        public T GetFirstBySpec(ISpecification<T> spec)
        {
            return GetQueryableBySpec(spec).FirstOrDefault();
        }

        public T GetFirstBySpecAsNoTracking(ISpecification<T> spec)
        {
            return GetQueryableBySpec(spec).AsNoTracking().FirstOrDefault();
        }

        public async Task<T> GetFirstBySpecAsync(ISpecification<T> spec)
        {
            return await GetQueryableBySpec(spec).FirstOrDefaultAsync();
        }
        public async Task<T> GetFirstBySpecAsyncAsNoTracking(ISpecification<T> spec)
        {
            return await GetQueryableBySpec(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public List<T> GetList(ISpecification<T> spec)
        {
            var queryableResult = GetQueryableBySpec(spec);

            // return the result of the query using the specification's criteria expression
            return queryableResult.ToList();
        }

        public async Task<List<T>> GetListAsync(ISpecification<T> spec)
        {
            var queryableResult = GetQueryableBySpec(spec);

            // return the result of the query using the specification's criteria expression
            return await queryableResult.ToListAsync();
        }

        public int CountBySpec(ISpecification<T> spec)
        {
            return _dbContext.Set<T>().AsQueryable().Where(spec.Criteria).Count();
        }

        public async Task<int> CountBySpecAsync(ISpecification<T> spec)
        {
            return await _dbContext.Set<T>().AsQueryable().Where(spec.Criteria).CountAsync();
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetQueryableBySpec(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResult = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            queryableResult = spec.IncludeStrings
                .Aggregate(queryableResult,
                    (current, include) => current.Include(include));

            queryableResult = queryableResult.Where(spec.Criteria);

            return queryableResult;
        }

        public IQueryable<T> GetQueryable()
        {
            var queryableResult = _dbContext.Set<T>().AsQueryable();
            return queryableResult;
        }

        public void AddNotSaveChanges(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public async Task AddNotSaveChangesAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void UpdateNotSaveChanges(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void RemoveNotSaveChanges(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();

            return entities;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateRange(List<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public void AddOrUpdate(T entity)
        {
            if (entity.IsTransient())
                Add(entity);
            else
                Update(entity);
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            if (entity.IsTransient())
                await AddAsync(entity);
            else
                await UpdateAsync(entity);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveContextAsync();
        }

        public async Task RemoveByIdAsync(int id)
        {
            var entity = _dbContext.Set<T>().SingleOrDefault(m => m.Id == id);
            _dbContext.Set<T>().Remove(entity);
            await SaveContextAsync();
        }

        public void RemoveRange(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task RemoveRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveContextAsync() => await _dbContext.SaveChangesAsync();

        public void SaveContext() => _dbContext.SaveChanges();


        public IExecutionStrategy CreateExecutionStrategy() => _dbContext.Database.CreateExecutionStrategy();
        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();
        public async Task BulkInsertAsNoTrackingAsync(List<T> entities)
        {
            await _dbContext.BulkInsertAsync(entities);
        }
        public async Task BulkInsertOrUpdateAsync(List<T> entities)
        {
            var bulkConfig = new BulkConfig
            {
                PreserveInsertOrder = true,
                SetOutputIdentity = true
            };
            await _dbContext.BulkInsertOrUpdateAsync(entities, bulkConfig);
        }
    }
}
