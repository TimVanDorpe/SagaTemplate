 
using HC.Answer.Application.Command;
using HC.Answer.Domain.Aggregate.Prediction;
using HC.Answer.Infrastructure.DomainPersistence.Repository;
using HC.Answer.Processor.Command;
using HC.Answer.Processor.Saga;
using HC.Answer.Processor.Saga.Command;
using HC.Answer.Processor.Saga.ReplyMessage;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HC.Answer.Processor.UnitTests
{
    public class PredictAnswerE2ETests : TestFixture<Startup>
    {    
        [Fact]
        public void PredictAnswer_ShouldComplete()
        {
            // Arrange
            var appSettings = Scope.Resolve<AppSettings>();
            var tenantUniqueId = Guid.Parse("");

            var command = new PredictAnswerCMD(
               tenantUniqueId: tenantUniqueId,
               correlationUniqueId: Guid.NewGuid(),
               userUniqueId: Guid.NewGuid() 
                );

            command.__s = command.ToJsonString().ToHmac512Signature(
                    key: appSettings.Security.Signature.Key,
                    salt: appSettings.Security.Signature.Salt
                    );

            // Act and Assert
            NServiceBus.Testing.Test.Saga<PredictAnswerSAGA>()
                .ExpectSend<PredictAnswerCMD>()
                .When(sagaIsInvoked: (saga, context) => saga.Handle(command, context))
                .ExpectReply<PredictAnswer1RM>()
                .ExpectSend<PredictAnswer2CMD>()               
                .ExpectSagaCompleted()
                ;
        }

        [Fact]
        public async Task PredictAnswerHandlers_ShouldReply()
        {
            // Arrange
            var appSettings = Scope.Resolve<AppSettings>();
            var repository = Scope.Resolve<PredictionRepository>();          
            var tenantUniqueId = TenantUniqueId.Create("");            
            var correlationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

         // Act & Assert
 
         // PredictAnswer1CMD
            NServiceBus.Testing.Test.Handler(Scope.Resolve<NsbMessageProcessor<PredictAnswer1CMD, PredictAnswer1CV, PredictAnswer1CH>>())             
                .OnMessage<PredictAnswer1CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;  
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
 
         // PredictAnswer2CMD
            NServiceBus.Testing.Test.Handler(Scope.Resolve<NsbMessageProcessor<PredictAnswer2CMD, PredictAnswer2CV, PredictAnswer2CH>>())             
                .OnMessage<PredictAnswer2CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;  
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
  
        }       
    }
}
