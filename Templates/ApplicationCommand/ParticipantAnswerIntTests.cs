using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HC.Answer.Application.Models.InputModel.Answer;
using HC.Answer.Infrastructure.DomainPersistence.Repository.Answer;
using Xunit;
using Xunit.Abstractions;
namespace HC.Answer.Application.IntegrationTests.Answer
{
    public class ParticipantAnswer_IntTest
    {
        static ParticipantAnswer_IntTest()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;

        public ParticipantAnswer_IntTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        

        [Fact(Skip = "only manual")]
        //[Fact]
        public async Task ParticipantAnswer_ShouldBeValid()
        {
            // Arrange
            var answerAs = ObjectContainer.Resolve<AnswerAS>();            

            var userId = Guid.Parse("88B79954-98E6-48F6-B350-ACE49D98130C");
            var tenantId = Guid.Parse("");

            var model = new ParticipantAnswerIM()
            {
			 Text = ,
			 FrameworkUniqueId = ,
			 CategoryUniqueId = ,
             CorrelationUniqueId = Guid.NewGuid(),           
             TenantUniqueId = tenantId
            };
            var result = await answerAs.ParticipantAnswerAsync(userId, model);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
