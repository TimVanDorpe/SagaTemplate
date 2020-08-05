using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HC.Swatson.Application.Models.InputModel.Touchpoint;
using HC.Swatson.Infrastructure.DomainPersistence.Repository.Touchpoint;
using Xunit;
using Xunit.Abstractions;
namespace HC.Swatson.Application.IntegrationTests.Touchpoint
{
    public class AddSectorToTouchpoint_IntTest
    {
        static AddSectorToTouchpoint_IntTest()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;

        public AddSectorToTouchpoint_IntTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        

        [Fact(Skip = "only manual")]
        //[Fact]
        public async Task AddSectorToTouchpoint_ShouldBeValid()
        {
            // Arrange
            var touchpointAs = ObjectContainer.Resolve<TouchpointAS>();            

            var userId = Guid.Parse("88B79954-98E6-48F6-B350-ACE49D98130C");
            var tenantId = Guid.Parse("");

            var model = new AddSectorToTouchpointIM()
            {
			 TouchpointUniqueId = ,
			 SectorUniqueId = ,
             CorrelationUniqueId = Guid.NewGuid(),           
             TenantUniqueId = tenantId
            };
            var result = await touchpointAs.AddSectorToTouchpointAsync(userId, model);

            // Assert
            Assert.True(result.IsValid);
        }
      
    }
}
