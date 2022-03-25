using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using HC.Answer.Application.Models.InputModel;
using HC.Answer.Infrastructure.DomainPersistence.Repository;
using Xunit;
using HC.Common.UnitTesting;
using Xunit.Abstractions;
namespace HC.Answer.Application.IntegrationTests
{
    public class AnswerSurvey_IntTest : TestFixture<Startup>
    {   

        [Fact(Skip = "only manual")]
        //[Fact]
        public async Task AnswerSurvey_ShouldBeValid()
        {
            // Arrange
            var answerSurveyASAs = Scope.Resolve<AnswerSurveyASAS>();            

            var userId = Guid.Parse("88B79954-98E6-48F6-B350-ACE49D98130C");
            var tenantId = Guid.Parse("");

            var model = new AnswerSurveyIM()
            {
			 AnswerSurvey = ,
             CorrelationUniqueId = Guid.NewGuid(),           
             TenantUniqueId = tenantId
            };
            var result = await answerSurveyASAs.AnswerSurvey(userId, model);

            // Assert
            Assert.True(result.IsValid);
        }
      
    }
}
