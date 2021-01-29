using HC.Core.Test.Setup;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using HC.Common.UnitTesting;

namespace HC.Core.Application.IntegrationTests.ParticipantAttributeTypeKey
{
    public class ParticipantAttributeTypeKeyGetAllTests : TestFixture<Startup>
    {

        private readonly ITestOutputHelper output;

        public ParticipantAttributeTypeKeyGetAllTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact(Skip = "only manual")]
        //[Fact]
        public async Task GetParticipantAttributeTypeKeyGetAll()
        {
            // Arrange
            var service = Scope.Resolve<ParticipantAttributeTypeKeyAS>();

            var userId = Guid.Parse("88B79954-98E6-48F6-B350-ACE49D98130C");
            var tenantId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291");

            var result = await service.ParticipantAttributeTypeKeyGetAll(tenantId, Guid.NewGuid(), userId);

            // Assert
            Assert.True(result.IsValid);
        }

    }
}
