using HC.Common.UnitTesting;
using HC.Core.Infrastructure.DomainPersistence.Repository.ParticipantAttributeTypeKey;
using HC.Core.Test.Setup.Builders;
using System;
using System.Linq;
using Xunit;
using HC.Common.UnitTesting.Builders;
using System.Threading.Tasks;

namespace HC.Core.Application.UnitTests.ParticipantAttributeTypeKey
{
    public class ParticipantAttributeTypeKeyGetAllTests : TestFixture<Startup>
    {            
        [Fact]
        public async Task CanParticipantAttributeTypeKeyGetAll()
        {
            var service = Scope.Resolve<ParticipantAttributeTypeKeyAS>();

            var tenantId = Guid.Parse("17DBC831-4CF2-4BD1-B0B3-09E17D605291");
            var uniqueId = Guid.Parse("");

            var result = await service.ParticipantAttributeTypeKeyGetAll(Guid.NewGuid(), tenantId, Guid.NewGuid(), uniqueId);

            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }
    }
}
