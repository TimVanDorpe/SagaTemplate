


using System;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using HC.Isaac.Domain.Aggregate.PosWordString;
using Xunit;


namespace HC.Isaac.Domain.UnitTests.PosWordString
{
    public class PosWordStringCreateTests
    {
        static PosWordStringCreateTests()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }
        
        [Fact]
        public void CreateNewPosWordString_WithValidProperties_ShouldCreateAValidAggregate()
        {
            var poswordstring = PosWordStringAR.Create(
                tenantUniqueId:Guid.NewGuid().ToTenantUniqueId()
                ,uniqueId: new UniqueId(Guid.NewGuid())
				,text :
				,language :
				,defaultpospolarity :
				);

            Assert.NotNull(poswordstring);
        }       
    }
}
