     
using System;
using System.Threading.Tasks;
using HC.Answer.Processor.Saga.State;
using HC.Common;
using HC.Common.Processor.NSB;
using NServiceBus;
using HC.Answer.Domain.Aggregate.Prediction;
using HC.Answer.Infrastructure.DomainPersistence.Repository;
using HC.Common.Processor.Logging;
using HC.Common.Infrastructure.RedisCache;

namespace HC.Answer.Processor.Saga.Command
{
    public class PredicteAnswer1CH : NsbMessageHandler<PredicteAnswer1CMD, PredictionAR>
    {       
        private readonly PredictionRepository _repository;

        public PredicteAnswer1CH(
            PredictionRepository repository,       
            AppSettings appSettings, IProcessLogger logger, 
            NServiceBusPublisher nsbPublisher, IRedisCacheManager cacheManager
            )
             : base(appSettings, logger, nsbPublisher, cacheManager,repository)
        {
            // Conditions
            Condition.Requires(repository).IsNotNull();

            //Init
            _repository = repository;
        }

        public override async Task ExecuteAsync(PredicteAnswer1CMD command, IMessageHandlerContext context)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();            
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            var sw = this.Log.MessageHandlerStart( command.UserUniqueId.ToUniqueId(), command.CorrelationUniqueId.ToUniqueId(), command);

            // TODO : HANDLE IT 

           // Reply back to the saga 
           await context.SendReplyMessageAsync(command.MapToReplyMessage(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);           

            // Log line
            this.Log.MessageHandlerFinish(sw, command.CorrelationUniqueId.ToUniqueId(), command);
        }

        public override AsyncPollingInfo ConfigureAsyncPolling()
        {
            return AsyncPollingInfo.Create(
                 step: 1,
                 total: PredicteAnswerState.Total,
                 message: PredicteAnswerState.Step1);
        }
    }
}      
