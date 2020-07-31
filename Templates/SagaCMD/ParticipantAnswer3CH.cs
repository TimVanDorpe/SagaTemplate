     
using System;
using System.Threading.Tasks;
using HC.Answer.Processor.Saga.State;
using HC.Common;
using HC.Common.Processor.NSB;
using NServiceBus;
using HC.Answer.Domain.Aggregate.Answer;
using HC.Answer.Infrastructure.DomainPersistence.Repository;

namespace HC.Answer.Processor.Saga.Command
{
    public class ParticipantAnswer3CH : NsbMessageHandler<ParticipantAnswer3CMD, AnswerAR>
    {       

        public ParticipantAnswer3CH(
            AnswerRepository repository         
            )
             : base(repository)
        {
            // Conditions
            Condition.Requires(repository).IsNotNull();
        }

        public override async Task ExecuteAsync(ParticipantAnswer3CMD command, IMessageHandlerContext context)
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
                 total: ParticipantAnswerState.Total,
                 message: ParticipantAnswerState.Step3);
        }
    }
}      
