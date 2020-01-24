using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Isaac.Application.Models.InputModel.PosWordString;
using HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString;
using Xunit;
using Xunit.Abstractions;

namespace HC.Isaac.Application.UnitTests.PosWordString
{
    public class PosWordStringCreateTest
    {
        static PosWordStringCreateTest()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;
        private readonly Guid tenantUniqueId = Guid.Parse("63f8042d-c96f-4b90-8076-c4e9a8db8303");

        public PosWordStringCreateTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task CreatePosWordString_ShouldReturnIsValidResult()
        {
            // Arrange
            var poswordstringAS = ObjectContainer.Resolve<PosWordStringAS>();
            var poswordstringRep = ObjectContainer.Resolve<PosWordStringRepository>();           
            var model = new CreateAPosWordStringIM()
            {
                CorrelationUniqueId = Guid.NewGuid(),
                TenantUniqueId = tenantUniqueId,
			 Text = ,
			 Language = ,
			 DefaultPosPolarity = ,
            };
            // Act
            var result = await poswordstringAS.CreateAsync(Guid.NewGuid(), model);

            // Assert
            Assert.True(result.IsValid);
        }      
    }
}
