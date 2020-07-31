            
using System;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Processor.NSB;
using HC.Integration.Domain.ValueObjects;
using HC.Integration.Events.Core;
using NServiceBus;

namespace HC.InsiderMetrics.Processor.Event
{

    public class SectorUpdatedEH : NsbMessageHandler< SectorUpdatedIE, SectorAR>
    {
        public SectorUpdatedEH(SectorRepository repository)
            : base(repository)
        {

        }
        public override async Task ExecuteAsync(SectorUpdatedIE command, IMessageHandlerContext context)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UniqueId.Requires(nameof(command.UniqueId)).IsNotEqualTo(new Guid());
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            var sw = this.Log.MessageHandlerStart(command.UserUniqueId.ToUniqueId(), command.CorrelationUniqueId.ToUniqueId(), command);
            
            // TODO : 
            // Create aggregate
            var aggregate = 
           
            // Save touchpoint to persistence
            await this.Repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate, context);

            // Log line
            this.Log.MessageHandlerFinish(sw, command.CorrelationUniqueId.ToUniqueId(), command);
        }

        public override AsyncPollingInfo ConfigureAsyncPolling()
        {
            return null;
        }
    }
}

