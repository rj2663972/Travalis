using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.EntityConfigurations;
using Domain.Entities.TicketAgg;

namespace Data.DataContext
{
    public class TravalisDataContext : DbContext, ITravalisDataContext
    {
        private readonly IServiceProvider _services;
        public TravalisDataContext(DbContextOptions<TravalisDataContext> options,
            IServiceProvider services)
            : base(options)
        {
            _services = services;
        }

        #region DbSets
        public DbSet<Ticket> Tickets { get; set; }
        #endregion

        #region [ Repository Getter ]

        [DebuggerStepThrough]
        public IRepository<TEntity> GetRepositoryByEntity<TEntity>()
                   where TEntity : BaseEntity
        {
            var entityTypeName = $"I{typeof(TEntity).Name}Repository";
            var namespaceName = typeof(TEntity).Namespace.Split(".Entities")[0];
            var repositoryTypeName = $"{typeof(TEntity).Namespace}.Interfaces.{entityTypeName}, {namespaceName}";

            Type repositoryType = Type.GetType(typeName: repositoryTypeName, ignoreCase: true, throwOnError: false);
            if (repositoryType == null)
                repositoryType = typeof(IRepository<TEntity>);

            return _services.GetService(repositoryType) as IRepository<TEntity>;
        }

        [DebuggerStepThrough]
        public TRepository GetRepository<TRepository>()
            where TRepository : class =>
            _services.GetService(typeof(TRepository)) as TRepository;

        #endregion

        public DbConnection GetDbConnection() => Database.GetDbConnection();
        public void OpenDbConnection() => Database.OpenConnection();
        public void CloseDbConnection() => Database.CloseConnection();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ConfigureWarnings(builder =>
            {
                builder.Throw(RelationalEventId.MultipleCollectionIncludeWarning);
            });
        }

        [DebuggerStepThrough]
        private void ApplyIDeletableEntity()
        {
            var auditableEntries = ChangeTracker.Entries()
                .Where(entry => typeof(IDeletableEntity).IsAssignableFrom(entry.Entity.GetType()) &&
                        entry.State == EntityState.Deleted);

            foreach (var item in auditableEntries)
            {
                item.Property(DeletableEntityFields.IsDeleted).CurrentValue = true;
                item.State = EntityState.Modified;
            }
        }

        [DebuggerStepThrough]
        private void ApplyIAuditableEntityInformation()
        {
            var auditableEntries = ChangeTracker.Entries()
                .Where(entry => typeof(IAuditableEntity).IsAssignableFrom(entry.Entity.GetType()) &&
                            (entry.State == EntityState.Added || entry.State == EntityState.Modified));

            foreach (var item in auditableEntries)
            {
                if (item.State == EntityState.Added)
                {
                    item.Property(AuditableEntityFields.CreateOn).CurrentValue = DateTime.UtcNow.AddHours(2);
                }

                item.Property(AuditableEntityFields.UpdateOn).CurrentValue = DateTime.UtcNow.AddHours(2);
            }
        }

        [DebuggerStepThrough]
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyIDeletableEntity();
            ApplyIAuditableEntityInformation();
            return await base.SaveChangesAsync(cancellationToken);

        }

        [DebuggerStepThrough]
        public override int SaveChanges()
        {
            ApplyIDeletableEntity();
            ApplyIAuditableEntityInformation();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeConfigurations.DefaultConfiguration(builder);
            //ModelCreatingConfigurations(builder);
            //ModelCreatingSeeding(builder);
        }
    }
}
