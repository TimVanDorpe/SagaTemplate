using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Swatson.Application.Models.InputModel.Touchpoint;
using HC.Swatson.Infrastructure.DomainPersistence.Repository.Touchpoint;
using Xunit;
using Xunit.Abstractions;

namespace HC.Swatson.Application.UnitTests.Touchpoint
{
    public class AddSectorToTouchpoint
    {
        static AddSectorToTouchpoint()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;       

        public AddSectorToTouchpoint(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task AddSectorToTouchpoint_ShouldReturnIsValidResult()
        {
            // Arrange
            var touchpointAS = ObjectContainer.Resolve<TouchpointAS>();
            var touchpointRep = ObjectContainer.Resolve<TouchpointRepository>();           
            var tenantUniqueId = Guid.Parse("");

            var model = new AddSectorToTouchpointIM()
            {
                CorrelationUniqueId = Guid.NewGuid(),
                TenantUniqueId = tenantUniqueId,
			 TouchpointUniqueId = ,
			 SectorUniqueId = ,
            };
            // Act
            var result = await touchpointAS.AddSectorToTouchpointAsync(Guid.NewGuid(), model);

            // Assert
            Assert.True(result.IsValid);
        }      
    }
}
