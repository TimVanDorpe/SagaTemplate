using System.Diagnostics;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Core.Application.Models.ViewModel.ParticipantAttributeTypeKey;
using HC.Core.Domain.Aggregate.ParticipantAttributeTypeKey;
using HC.Core.Infrastructure.DomainPersistence.Repository.ParticipantAttributeTypeKey;

namespace HC.Core.Application.Query
{
    public class ParticipantAttributeTypeKeyGetAllQH : QueryHandler<ParticipantAttributeTypeKeyGetAllQRY, ParticipantAttributeTypeKeyVM>
    {

        private readonly ParticipantAttributeTypeKeyRepository _participantAttributeTypeKeyRepository;
        private readonly MappingProcessor _mappingProcessor;

        public ParticipantAttributeTypeKeyGetAllQH (ILogger logger,  MappingProcessor mappingProcessor, ParticipantAttributeTypeKeyRepository participantAttributeTypeKeyRepository) : base(logger)
        {
            participantAttributeTypeKeyRepository.Requires().IsNotNull();
            mappingProcessor.Requires().IsNotNull();

            _participantAttributeTypeKeyRepository = participantAttributeTypeKeyRepository;
            _mappingProcessor = mappingProcessor;
        }

        protected override async Task<ParticipantAttributeTypeKeyVM> ExecuteAsync(ParticipantAttributeTypeKeyGetAllQRY query)
        {
            // Conditions
            query.Requires(nameof(query)).IsNotNull();

            // Log line
            this.Log.Debug("Handle query", query.CorrelationUniqueId.ToUniqueId(), query);

            // Log line
            this.Log.Debug("Execute query", query.CorrelationUniqueId.ToUniqueId());

            // Start elapse time
            var sw = Stopwatch.StartNew();

            // Execute query
            var aggregate = await this._participantAttributeTypeKeyRepository.FindAsync(Loading.Full, query.TenantUniqueId.ToTenantUniqueId(), query.UniqueId.ToUniqueId());

            // Stop elapse time
            sw.Stop();

            // Log line
            this.Log.Debug("Executed query", query.CorrelationUniqueId.ToUniqueId(), sw.Elapsed);

            // Log line
            this.Log.Debug("Map result to VM", query.CorrelationUniqueId.ToUniqueId());

            // Map Aggregate to Viewmodel
           var result = _mappingProcessor.Map<ParticipantAttributeTypeKeyAR, ParticipantAttributeTypeKeyVM>(aggregate);

            // Log line
            this.Log.Debug("Handling query is done", query.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
