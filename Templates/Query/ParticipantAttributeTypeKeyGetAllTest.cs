using HC.Core.Application;
using HC.Core.Application.UnitTests;
using System;
using System.Threading.Tasks;
using NServiceBus.Testing;
using Xunit;
using HC.Common.UnitTesting;

namespace HC.Core.Application.UnitTests.ParticipantAttributeTypeKey
{
    public class ParticipantAttributeTypeKeyGetAllTests
    {
        static ParticipantAttributeTypeKeyTests()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }       

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
