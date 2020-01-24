
using System.Collections.Generic;
using HC.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Isaac.Application.Models.ViewModel.PosWordString;
using HC.Isaac.Domain.Aggregate.PosWordString;
using HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString;


namespace HC.Isaac.Application.Query
{
    public class PosWordStringGetAllQH : QueryHandler<PosWordStringGetAllQRY, IEnumerable<PosWordStringVM>>
    {
        private readonly PosWordStringRepository repository;

        public PosWordStringGetAllQH(
            ILogger logger,
           PosWordStringRepository repository
        )
            : base(logger)
        {
            Condition.Requires(repository, nameof(repository)).IsNotNull();

            this.repository = repository;
        }

        protected async override Task<IEnumerable<PosWordStringVM>> ExecuteAsync(PosWordStringGetAllQRY query)
        {
            // Conditions
            Condition.Requires(query, nameof(query)).IsNotNull();

            // Log line
            this.Log.Debug("Handle query", query.CorrelationUniqueId.ToUniqueId(), query);

            // Log line
            this.Log.Debug("Execute query", query.CorrelationUniqueId.ToUniqueId());

            // Start elapse time
            var sw = Stopwatch.StartNew();

            // Execute query
            var aggregates = await this.repository.FindAllAsync(Loading.Full, query.TenantUniqueId.ToTenantUniqueId());

            // Stop elapse time
            sw.Stop();

            // Log line
            this.Log.Debug("Executed query", query.CorrelationUniqueId.ToUniqueId(), sw.Elapsed);

            // Log line
            this.Log.Debug("Map result to VM", query.CorrelationUniqueId.ToUniqueId());

            // Map Aggregate to Viewmodel
            var result = ObjectContainer.Resolve<MappingProcessor>().Map<IEnumerable<PosWordStringVM>>(aggregates);

            // Log line
            this.Log.Debug("Handling query is done", query.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
