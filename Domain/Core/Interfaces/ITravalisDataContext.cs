using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Domain.Core.Interfaces
{
    public interface ITravalisDataContext
    {
        IRepository<TEntity> GetRepositoryByEntity<TEntity>() where TEntity : BaseEntity;
        TRepository GetRepository<TRepository>() where TRepository : class;
        DbConnection GetDbConnection();
        void OpenDbConnection();
        void CloseDbConnection();
    }
}
