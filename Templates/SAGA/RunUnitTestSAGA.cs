using System;
using System.Threading.Tasks;
using HC.Swatson.Application.Command;
using HC.Swatson.Processor.Saga.ReplyMessage;
using HC.Swatson.Processor.Saga.State;
using HC.Common;
using HC.Common.Processor.Logging;
using HC.Common.Processor.Security;
using NServiceBus;

namespace HC.Swatson.Processor.Saga
{
    public class RunUnitTestSAGA :
        Saga<RunUnitTestSD>, 
        IAmStartedByMessages<RunUnitTestCMD>,
		 IHandleMessages<RunUnitTest1RM> ,        
		 IHandleMessages<RunUnitTest2RM> ,        
		 IHandleMessages<RunUnitTest3RM>         
        
    {
        private readonly IProcessLogger Log;
        private readonly AppSettings AppSettings;

        // In order to test the SAGA, the constructor can't contain any parameter. See https://docs.particular.net/nservicebus/testing/fluent 
        // for more information about this topic.
        public RunUnitTestSAGA()
        {
            // Init
            this.Log = ObjectContainer.Resolve<IProcessLogger>();
            this.AppSettings = ObjectContainer.Resolve<AppSettings>();

            // Conditions
            Condition.Requires(this.Log, nameof(this.Log)).IsNotNull();
            Condition.Requires(this.AppSettings, nameof(this.AppSettings)).IsNotNull();
        }

        public RunUnitTestSAGA(
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

          public async Task Handle(RunUnitTestCMD command, IMessageHandlerContext context)
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
            this.Data.State = RunUnitTestSS.Start;
            this.Data.Command = command;
            this.Data.Identifier = command.CorrelationUniqueId;
            

            // Send CreateAUser1CMD on the context 
            await context.SendCommandAsync(command.MapToCommand() , this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }


		
          public async Task Handle(RunUnitTest1RM message, IMessageHandlerContext context)
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
            this.Data.State = RunUnitTestSS.Step1Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
		
          public async Task Handle(RunUnitTest2RM message, IMessageHandlerContext context)
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
            this.Data.State = RunUnitTestSS.Step2Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
		
          public async Task Handle(RunUnitTest3RM message, IMessageHandlerContext context)
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
            this.Data.State = RunUnitTestSS.Step3Done;
       
            // Send command on the context
            await context.SendCommandAsync(message.MapToCommand(), this.AppSettings.Security.Signature.Key, this.AppSettings.Security.Signature.Salt);

            // Set as complete
            await Task.CompletedTask;
        }     
   

   protected override void ConfigureHowToFindSaga(SagaPropertyMapper<RunUnitTestSD> mapper)
        {
            mapper.ConfigureMapping<RunUnitTestCMD>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);
	
            mapper.ConfigureMapping<RunUnitTest1RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
	
            mapper.ConfigureMapping<RunUnitTest2RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
	
            mapper.ConfigureMapping<RunUnitTest3RM>(cmd => cmd.CorrelationUniqueId)
               .ToSaga(sd => sd.Identifier);   
   

        }
    }
}
