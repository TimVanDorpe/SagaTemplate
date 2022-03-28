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
    public class PredictAnswerSAGA :
        Saga<PredictAnswerSD>, 
        IAmStartedByMessages<PredictAnswerCMD>,
		 IHandleMessages<PredictAnswer1RM> ,        
		 IHandleMessages<PredictAnswer2RM>         
        
    {
        private readonly IProcessLogger _logger;
        private readonly AppSettings _appSettings;

        public PredictAnswerSAGA(IProcessLogger logger, AppSettings appSettings)
        {
          // Conditions
            logger.Requires().IsNotNull();
            appSettings.Requires().IsNotNull();

            // Init
            _logger = logger;
            _appSettings = appSettings;
        }        

          public async Task Handle(PredictAnswerCMD command, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            command.Requires(nameof(command)).IsNotNull();          
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            _logger.SagaStart( command.UserUniqueId.ToUniqueId(), command.CorrelationUniqueId.ToUniqueId(), command);

            // Check if signature is valid
            command.ValidateCommand(_appSettings.Security.Signature.Key, _appSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = PredictAnswerSS.Start;
            this.Data.Command = command;
            this.Data.Identifier = command.CorrelationUniqueId;
            

            // Send PredictAnswerCMD on the context 
            await context.SendCommandAsync(command.MapToCommand() , _appSettings.Security.Signature.Key, _appSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }


		
          public async Task Handle(PredictAnswer1RM message, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            message.Requires(nameof(message)).IsNotNull();
            message.UserUniqueId.Requires(nameof(message.UserUniqueId)).IsNotEqualTo(new Guid());
            message.TenantUniqueId.Requires(nameof(message.TenantUniqueId)).IsNotEqualTo(new Guid());
            message.CorrelationUniqueId.Requires(nameof(message.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            _logger.SagaStep(message.CorrelationUniqueId.ToUniqueId(), message);

            // Check if signature is valid
            message.ValidateReplyMessage(_appSettings.Security.Signature.Key, _appSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = PredictAnswerSS.Step1Done;

                   // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), _appSettings.Security.Signature.Key, _appSettings.Security.Signature.Salt);
           
                     
            // Set as complete
            await Task.CompletedTask;
        }     
		
          public async Task Handle(PredictAnswer2RM message, IMessageHandlerContext context)
        {
            // Conditions
            context.Requires(nameof(context)).IsNotNull();
            message.Requires(nameof(message)).IsNotNull();
            message.UserUniqueId.Requires(nameof(message.UserUniqueId)).IsNotEqualTo(new Guid());
            message.TenantUniqueId.Requires(nameof(message.TenantUniqueId)).IsNotEqualTo(new Guid());
            message.CorrelationUniqueId.Requires(nameof(message.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Init log
            _logger.SagaStep(message.CorrelationUniqueId.ToUniqueId(), message);

            // Check if signature is valid
            message.ValidateReplyMessage(_appSettings.Security.Signature.Key, _appSettings.Security.Signature.Salt);

            // Update saga entity
            this.Data.State = PredictAnswerSS.Step2Done;

              
           // Mark saga as completed
           MarkAsComplete();
                    
            // Set as complete
            await Task.CompletedTask;
        }     
   

   protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PredictAnswerSD> mapper)
        {
            mapper.ConfigureMapping<PredictAnswerCMD>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);
	
            mapper.ConfigureMapping<PredictAnswer1RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
	
            mapper.ConfigureMapping<PredictAnswer2RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
   

        }
    }
}
