
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HC.Isaac.Application.Models.InputModel.PosWordString;
using HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString;
using Xunit;
using Xunit.Abstractions;
namespace HC.Isaac.Application.IntegrationTests.PosWordString
{
    public class CreateAPosWordString_IntTest
    {
        static CreateAPosWordString_IntTest()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;

        public CreateAPosWordString_IntTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        

        [Fact(Skip = "only manual")]
        //[Fact]
        public async Task CreateAPosWordString_ShouldCreateAPosWordString()
        {
            // Arrange
            var poswordstringAs = ObjectContainer.Resolve<PosWordStringAS>();
            

            var userId = Guid.Parse("88B79954-98E6-48F6-B350-ACE49D98130C");
            var tenantId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291");
            var model = new CreateAPosWordStringIM()
            {
			 Text = ,
			 Language = ,
			 DefaultPosPolarity = ,
                CorrelationUniqueId = Guid.NewGuid(),           
                TenantUniqueId = tenantId
            };
            var poswordstring = await poswordstringAs.CreateAsync(userId, model);

            // Assert
            Assert.True(poswordstring.IsValid);
        }
      
    }
}
