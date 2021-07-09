using Domain.Core.Entities;
using Domain.Core.Specifications;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        Task<T> GetSingleBySpecAsync(ISpecification<T> spec);
        Task<T> GetFirstBySpecAsync(ISpecification<T> spec);
        Task<T> GetFirstBySpecAsyncAsNoTracking(ISpecification<T> spec);
        Task<List<T>> GetListAsync(ISpecification<T> spec);
        Task<int> CountBySpecAsync(ISpecification<T> spec);
        IQueryable<T> GetQueryableBySpec(ISpecification<T> spec);
        IQueryable<T> GetQueryable();
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Add entity in context, you MUST CALL SaveContextAsync after this, in order to persist entity in the database
        /// </summary>
        Task AddNotSaveChangesAsync(T entity);
        /// <summary>
        /// Update entity in context, you MUST CALL SaveContextAsync after this, in order to persist entity in the database
        /// </summary>
        void UpdateNotSaveChanges(T entity);
        /// <summary>
        /// Remove entity in context, you MUST CALL SaveContextAsync after this, in order to persist entity in the database
        /// </summary>
        void RemoveNotSaveChanges(T entity);
        /// <summary>
        /// Add entity in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Add range of entities in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task<List<T>> AddRangeAsync(List<T> entities);
        /// <summary>
        /// Update entity in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Update range of entities in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task UpdateRangeAsync(List<T> entities);
        /// <summary>
        /// Add or Update entity in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task AddOrUpdateAsync(T entity);
        /// <summary>
        /// Remove entity in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task RemoveAsync(T entity);
        /// <summary>
        /// Remove entity in context by id and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task RemoveByIdAsync(int id);
        /// <summary>
        /// Remove range of entities in context and SAVE ALL CHANGES CONTEXT
        /// </summary>
        Task RemoveRangeAsync(List<T> entities);
        /// <summary>
        /// Save all changes context, it's NOT NECESSARY to call this method, if you call AddAsync, AddRangeAsync, UpdateAsync, UpdateangeAsync, RemoveAsync or RemoveRangeAsync 
        /// </summary>
        Task SaveContextAsync();

        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction BeginTransaction();
        Task BulkInsertAsNoTrackingAsync(List<T> entities);
        Task BulkInsertOrUpdateAsync(List<T> entities);

    }
}
