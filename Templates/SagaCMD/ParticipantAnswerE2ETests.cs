 
using HC.Answer.Application.Command;
using HC.Answer.Domain.Aggregate.Answer;
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
    public class ParticipantAnswerE2ETests
    {
        static ParticipantAnswerE2ETests()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;

        public ParticipantAnswerE2ETests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ParticipantAnswer_ShouldComplete()
        {
            // Arrange
            var appSettings = ObjectContainer.Resolve<AppSettings>();

            var command = new ParticipantAnswerCMD(
               tenantUniqueId: Guid.Parse("424242D0-C217-4D75-81C0-D9A99439E416"),
               correlationUniqueId: Guid.NewGuid(),
               userUniqueId: Guid.NewGuid(),             
               uniqueId: UniqueId.Generate()
                );

            command.__s = command.ToJsonString().ToHmac512Signature(
                    key: appSettings.Security.Signature.Key,
                    salt: appSettings.Security.Signature.Salt
                    );

            // Act and Assert
            NServiceBus.Testing.Test.Saga<ParticipantAnswerSAGA>()
                .ExpectSend<ParticipantAnswerCMD>()
                .When(sagaIsInvoked: (saga, context) => saga.Handle(command, context))
                .ExpectReply<ParticipantAnswer1RM>()
                .ExpectSend<ParticipantAnswer2CMD>()               
                .ExpectSagaCompleted()
                ;
        }

        [Fact]
        public async Task ParticipantAnswerHandlers_ShouldReply()
        {
            // Arrange
            var appSettings = ObjectContainer.Resolve<AppSettings>();
            var repository = ObjectContainer.Resolve<AnswerRepository>();
            // TODO : correct tenant
            var tenantUniqueId = TenantUniqueId.Create("424242D0-C217-4D75-81C0-D9A99439E416");
            var uniqueId = UniqueId.Generate();
            var correlationId = Guid.NewGuid();
            var userId = Guid.NewGuid();

         // Act & Assert
 
         // ParticipantAnswer1CMD
            NServiceBus.Testing.Test.Handler<NsbMessageProcessor<ParticipantAnswer1CMD, ParticipantAnswer1CV, ParticipantAnswer1CH>>()
                .ExpectReply<ParticipantAnswer1RM>(message => message.UniqueId == uniqueId)
                .OnMessage<ParticipantAnswer1CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;                  
                    m.UniqueId = uniqueId;
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
 
         // ParticipantAnswer2CMD
            NServiceBus.Testing.Test.Handler<NsbMessageProcessor<ParticipantAnswer2CMD, ParticipantAnswer2CV, ParticipantAnswer2CH>>()
                .ExpectReply<ParticipantAnswer2RM>(message => message.UniqueId == uniqueId)
                .OnMessage<ParticipantAnswer2CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;                  
                    m.UniqueId = uniqueId;
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
 
         // ParticipantAnswer3CMD
            NServiceBus.Testing.Test.Handler<NsbMessageProcessor<ParticipantAnswer3CMD, ParticipantAnswer3CV, ParticipantAnswer3CH>>()
                .ExpectReply<ParticipantAnswer3RM>(message => message.UniqueId == uniqueId)
                .OnMessage<ParticipantAnswer3CMD>(m =>
                {
                    m.TenantUniqueId = tenantUniqueId.Value;
                    m.CorrelationUniqueId = correlationId;
                    m.UserUniqueId = userId;                  
                    m.UniqueId = uniqueId;
                    m.__s = m.ToJsonString().ToHmac512Signature(key: appSettings.Security.Signature.Key, salt: appSettings.Security.Signature.Salt);
                })
               ;
  
        }       
    }
}
