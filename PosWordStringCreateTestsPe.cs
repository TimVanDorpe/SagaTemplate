  using System;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Integration.Domain.ValueObjects;
using HC.Isaac.Domain.Aggregate.poswordstring;
using HC.Isaac.Infrastructure.DomainPersistence.UnitTests;
using HC.Isaac.Test.Setup;
using Xunit;
namespace HC.Isaac.Infrastructure.UnitTests.DomainPersistence.poswordstring
{
    public class poswordstringCreateTests
    {
        static poswordstringCreateTests()
        {
            if (Startup.IocConfig == null)
                Startup.IocConfig = Startup.Init();
        }

        [Fact]
        public async Task CreateNewposwordstring_WithValidProperties_ShouldCreateAValidAggregate()
        {
            // Arrange
            var repository = await Database.GetposwordstringRepository();
		    var text = ;
		    var language = ;
		    var defaultpospolarity = ;
            // Act
            var aggregate = PosWordStringAR.Create(tenantUniqueId, UniqueId.Create(Guid.NewGuid()) , text  , language  , defaultpospolarity );
            aggregate = await repository.SaveAsync(UniqueId.Create(Guid.NewGuid()), UniqueId.Create(Guid.NewGuid()), aggregate);
            
            // Assert
            Assert.NotNull(aggregate.UniqueId);
        }
    }
}
