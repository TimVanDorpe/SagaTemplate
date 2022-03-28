using System.Collections.Generic;
using HC.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using HC.Common.Infrastructure.DomainPersistence;
using HC.LegacySync.Application.Models.ViewModel.Survey;
using HC.LegacySync.Domain.Aggregate.Survey;
using HC.LegacySync.Infrastructure.DomainPersistence.Repository.Survey;


namespace HC.LegacySync.Application.Query
{
    public class SurveyGetAllQH : QueryHandler<SurveyGetAllQRY, IEnumerable<SurveyVM>>
    {
        private readonly SurveyRepository repository;

        public SurveyGetAllQH(
            ILogger logger,
           SurveyRepository repository
        )
            : base(logger)
        {
            Condition.Requires(repository, nameof(repository)).IsNotNull();

            repository = repository;
        }

        protected async override Task<IEnumerable<SurveyVM>> ExecuteAsync(SurveyGetAllQRY query)
        {
            // Conditions
            Condition.Requires(query, nameof(query)).IsNotNull();

            // Log line
            Log.Debug("Handle query", query.CorrelationUniqueId.ToUniqueId(), query);

            // Log line
            Log.Debug("Execute query", query.CorrelationUniqueId.ToUniqueId());

            // Start elapse time
            var sw = Stopwatch.StartNew();

            // Execute query
            var aggregates = await repository.FindAllAsync(Loading.Full, query.TenantUniqueId.ToTenantUniqueId());

            // Stop elapse time
            sw.Stop();

            // Log line
            Log.Debug("Executed query", query.CorrelationUniqueId.ToUniqueId(), sw.Elapsed);

            // Log line
            Log.Debug("Map result to VM", query.CorrelationUniqueId.ToUniqueId());

            // Map Aggregate to Viewmodel
            var result = ObjectContainer.Resolve<MappingProcessor>().Map<IEnumerable<SurveyVM>>(aggregates);

            // Log line
            Log.Debug("Handling query is done", query.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
