     
using System;
using System.Threading.Tasks;
using HC.Swatson.Processor.Saga.State;
using HC.Common;
using HC.Common.Processor.NSB;
using NServiceBus;

namespace HC.Swatson.Processor.Saga.Command
{
    public class RunUnitTest3CH : NsbMessageHandler<RunUnitTest3CMD, UnitTestAR>
    {       

        public RunUnitTest3CH(
            UnitTestRepository repository         
            )
             : base(repository)
        {
            // Conditions
            Condition.Requires(repository).IsNotNull();

            // Init
            this.Repository = repository;
        }

        public override async Task ExecuteAsync(RunUnitTest3CMD command, IMessageHandlerContext context)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UniqueId.Requires(nameof(command.UniqueId)).IsNotNull();
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
                 step: 3,
                 total: RunUnitTestState.Total,
                 message: RunUnitTestState.Step3);
        }
    }
}      
