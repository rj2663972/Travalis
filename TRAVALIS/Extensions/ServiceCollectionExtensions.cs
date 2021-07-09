using Data.DataContext;
using Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TRAVALIS.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyApplicationService(this IServiceCollection services)
        {
            Assembly[] assembliesToScan =
            {
                typeof(Application.Interfaces.ICacheAppService).Assembly,
                typeof(Data.DataContext.TravalisDataContext).Assembly
            };
            services.Scan(scan => scan
                    .FromAssemblies(assembliesToScan)
                    .AddClasses(classes => classes.AssignableTo<ITransientService>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IScopedService>())
                        .As<IScopedService>()
                        .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection ConfigureMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFramework(configuration)
                    .AddAutoMapper(GetOwnAssembliesWhereThereAreMappings());

            return services;
        }

        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services
              .AddDbContext<TravalisDataContext>(setup =>
              {
                  setup.UseSqlServer(configuration.GetConnectionString(nameof(TravalisDataContext)), setup =>
                  {
                      setup.MaxBatchSize(100);
                      setup.EnableRetryOnFailure();
                      setup.MigrationsAssembly(typeof(TravalisDataContext).Assembly.FullName);
                  });
              });

            return services.AddScoped<ITravalisDataContext, TravalisDataContext>();
        }

        private static List<Assembly> GetOwnAssembliesWhereThereAreMappings()
        {
            var travalisServiceDomain = AppDomain.CurrentDomain.Load($"{nameof(Domain)}");
            return new List<Assembly> { travalisServiceDomain };
        }
    }
}
