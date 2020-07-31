using System;
using System.Threading.Tasks;
using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.ReplyMessage;
using HC.Answer.Processor.Saga.State;
using HC.Common;
using HC.Common.Processor.Logging;
using HC.Common.Processor.Security;
using NServiceBus;

namespace HC.Answer.Processor.Saga
{
    public class ParticipantAnswerSAGA :
        Saga<ParticipantAnswerSD>, 
        IAmStartedByMessages<ParticipantAnswerCMD>,
		 IHandleMessages<ParticipantAnswer1RM> ,        
		 IHandleMessages<ParticipantAnswer2RM> ,        
		 IHandleMessages<ParticipantAnswer3RM>         
        
    {
        private readonly IProcessLogger Log;
        private readonly AppSettings AppSettings;

        // In order to test the SAGA, the constructor can't contain any parameter. See https://docs.particular.net/nservicebus/testing/fluent 
        // for more information about this topic.
        public ParticipantAnswerSAGA()
        {
            // Init
            this.Log = ObjectContainer.Resolve<IProcessLogger>();
            this.AppSettings = ObjectContainer.Resolve<AppSettings>();

            // Conditions
            Condition.Requires(this.Log, nameof(this.Log)).IsNotNull();
            Condition.Requires(this.AppSettings, nameof(this.AppSettings)).IsNotNull();
        }

        public ParticipantAnswerSAGA(
            IProcessLogger logger,
            AppSettings appSettings
            )
        {
            // Conditions
            Condition.Requires(logger, nameof(logger)).IsNotNull();
            Condition.Requires(appSettings, nameof(appSettings)).IsNotNull();

            // Init
            this.Log = logger;
            this.AppSettings = appSettings;
        }

          public async Task Handle(ParticipantAnswerCMD command, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            command.Requires(nameof(command)).IsNotNull();
            command.UniqueId.Requires(nameof(command.UniqueId)).IsNotNull();
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            this.Log.SagaStart( command.UserUniqueId.ToUniqueId(), command.CorrelationUniqueId.ToUniqueId(), command);

            // Check if signature is valid
            command.ValidateCommand(this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = ParticipantAnswerSS.Start;
            this.Data.Command = command;
            this.Data.Identifier = command.CorrelationUniqueId;
            

            // Send CreateAUser1CMD on the context 
            await context.SendCommandAsync(command.MapToCommand() , this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }


		
          public async Task Handle(ParticipantAnswer1RM message, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            message.Requires(nameof(message)).IsNotNull();
            message.UserUniqueId.Requires(nameof(message.UserUniqueId)).IsNotEqualTo(new Guid());
            message.TenantUniqueId.Requires(nameof(message.TenantUniqueId)).IsNotEqualTo(new Guid());
            message.CorrelationUniqueId.Requires(nameof(message.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            this.Log.SagaStep(message.CorrelationUniqueId.ToUniqueId(), message);

            // Check if signature is valid
            message.ValidateReplyMessage(this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = ParticipantAnswerSS.Step1Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
		
          public async Task Handle(ParticipantAnswer2RM message, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            message.Requires(nameof(message)).IsNotNull();
            message.UserUniqueId.Requires(nameof(message.UserUniqueId)).IsNotEqualTo(new Guid());
            message.TenantUniqueId.Requires(nameof(message.TenantUniqueId)).IsNotEqualTo(new Guid());
            message.CorrelationUniqueId.Requires(nameof(message.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            this.Log.SagaStep(message.CorrelationUniqueId.ToUniqueId(), message);

            // Check if signature is valid
            message.ValidateReplyMessage(this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = ParticipantAnswerSS.Step2Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
		
          public async Task Handle(ParticipantAnswer3RM message, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            message.Requires(nameof(message)).IsNotNull();
            message.UserUniqueId.Requires(nameof(message.UserUniqueId)).IsNotEqualTo(new Guid());
            message.TenantUniqueId.Requires(nameof(message.TenantUniqueId)).IsNotEqualTo(new Guid());
            message.CorrelationUniqueId.Requires(nameof(message.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            this.Log.SagaStep(message.CorrelationUniqueId.ToUniqueId(), message);

            // Check if signature is valid
            message.ValidateReplyMessage(this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = ParticipantAnswerSS.Step3Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
   

   protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ParticipantAnswerSD> mapper)
        {
            mapper.ConfigureMapping<ParticipantAnswerCMD>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);
	
            mapper.ConfigureMapping<ParticipantAnswer1RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
	
            mapper.ConfigureMapping<ParticipantAnswer2RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
	
            mapper.ConfigureMapping<ParticipantAnswer3RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
   

        }
    }
}
