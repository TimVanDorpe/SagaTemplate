using HC.LegacySync.Infrastructure.DomainPersistence.Repository;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.LegacySync.Infrastructure.Ioc
{
    public static class IocConfigInfrastructureExtensions
    {
        public static IocConfig RegisterInfrastructureComponents(this IocConfig iocConfig)
        {
            iocConfig.Builder.RegisterType(typeof(ApplicationDbContext));

            // Language- SQL Normal
            iocConfig.Builder.RegisterType(typeof(SurveyRepository), null, LifeStyle.Singleton);

            return iocConfig;
        }
        public static IContainerLifetimeScope InitializeNServiceBusForEntityFramework(this IContainerLifetimeScope scope)
        {
            // GET CONFIGURATION + DELEGATE
            var endpointConfiguration = scope.Resolve<EndpointConfiguration>();
            var endpointConfigurationInstance = scope.ResolveDelegate<EndpointConfiguration>();

            // Get pipeline
            var pipeline = endpointConfiguration.Pipeline;

            // Register all the unit of works
            pipeline.Register(new SurveyNsbUnitOfWork(PipelineRegistration(scope)), "SurveyNsbUnitOfWork");

            // Delegate the configuration
            endpointConfigurationInstance(endpointConfiguration);

            return scope;
        }

        private static System.Func<SynchronizedStorageSession, ApplicationDbContext> PipelineRegistration(IContainerLifetimeScope scope)
        {
            return storageSession =>
            {
                // Get NSB dbConnection
                var dbConnection = storageSession.SqlPersistenceSession().Connection;

                // Get DbContext with NSB DbConnection
                var dbContext = scope.Resolve<ApplicationDbContext>(new Dictionary<string, object>()
                  {
                   { "connection", dbConnection }
                  });

                // Use the same underlying ADO.NET transaction
                dbContext.Database.UseTransaction(storageSession.SqlPersistenceSession().Transaction);

                // Call SaveChanges before completing storage session
                storageSession.SqlPersistenceSession().OnSaveChanges(async _ => await dbContext.SaveChangesAsync().ConfigureAwait(false));

                return dbContext;
            };
        }
    }
}
  