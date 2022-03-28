 
using HC.Answer.Application.Command;
using HC.Answer.Domain.Aggregate.Tenant;
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
    public class PredicteAnswerE2ETests : TestFixture<Startup>
    {    
        [Fact]
        public void PredicteAnswer_ShouldComplete()
        {
            // Arrange
            var appSettings = Scope.Resolve<AppSettings>();
            var tenantUniqueId = Guid.Parse("");

            var command = new PredicteAnswerCMD(
               tenantUniqueId: tenantUniqueId,
               correlationUniqueId: Guid.NewGuid(),
               userUniqueId: Guid.NewGuid() 
                );

            command.__s = command.ToJsonString().ToHmac512Signature(
                    key: appSettings.Security.Signature.Key,
                    salt: appSettings.Security.Signature.Salt
                    );

            // Act and Assert
            NServiceBus.Testing.Test.Saga<PredicteAnswerSAGA>()
                .ExpectSend<PredicteAnswerCMD>()
                .When(sagaIsInvoked: (saga, context) => saga.Handle(command, context))
                .ExpectReply<PredicteAnswer1RM>()
                .ExpectSend<PredicteAnswer2CMD>()               
                .ExpectSagaCompleted()
                ;
        }

        [Fact]
        public async Task PredicteAnswerHandlers_ShouldReply()
        {
            // Arrange
            var appSettings = Scope.Resolve<AppSettings>();
            var repository = Scope.Resolve<TenantRepository>();          
            var tenantUniqueId = TenantUniqueId.Create("");            
            var correlationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

         // Act & Assert
 
         // PredicteAnswer1CMD
            NServiceBus.Testing.Test.Handler(Scope.Resolve<NsbMessageProcessor<PredicteAnswer1CMD, PredicteAnswer1CV, PredicteAnswer1CH>>())             
                .OnMessage<PredicteAnswer1CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;  
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
 
         // PredicteAnswer2CMD
            NServiceBus.Testing.Test.Handler(Scope.Resolve<NsbMessageProcessor<PredicteAnswer2CMD, PredicteAnswer2CV, PredicteAnswer2CH>>())             
                .OnMessage<PredicteAnswer2CMD>(m =>
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
