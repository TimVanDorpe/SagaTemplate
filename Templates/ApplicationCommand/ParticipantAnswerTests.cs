using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Answer.Application.Models.InputModel.Answer;
using HC.Answer.Infrastructure.DomainPersistence.Repository.Answer;
using Xunit;
using Xunit.Abstractions;

namespace HC.Answer.Application.UnitTests.Answer
{
    public class ParticipantAnswer
    {
        static ParticipantAnswer()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;       

        public ParticipantAnswer(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task ParticipantAnswer_ShouldReturnIsValidResult()
        {
            // Arrange
            var answerAS = ObjectContainer.Resolve<AnswerAS>();
            var answerRep = ObjectContainer.Resolve<AnswerRepository>();           
            var tenantUniqueId = Guid.Parse("");

            var model = new ParticipantAnswerIM()
            {
                CorrelationUniqueId = Guid.NewGuid(),
                TenantUniqueId = tenantUniqueId,
			 Text = ,
			 FrameworkUniqueId = ,
			 CategoryUniqueId = ,
            };
            // Act
            var result = await answerAS.ParticipantAnswerAsync(Guid.NewGuid(), model);

            // Assert
            Assert.True(result.IsValid);
        }      
    }
}
