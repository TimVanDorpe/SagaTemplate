
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HC.Isaac.Application.UnitTests.PosWordString
{
    public class PosWordStringFindAllQRYTests
    {
        static PosWordStringFindAllQRYTests()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        private readonly ITestOutputHelper output;

        public PosWordStringFindAllQRYTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task PosWordStringFindAll_ShouldReturnResult()
        {
            // Arrange
            var poswordstringAS = ObjectContainer.Resolve<PosWordStringAS>();
            var tenantUniqueId = Test.Setup.Helpers.Tenant.SuperTenantID;
            
            // Act
            var result = await poswordstringAS.GetAllAsync(tenantUniqueId, Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.True(result.IsValid && result.Object != null && result.Object.ToList().Any());
        }
    }
}
