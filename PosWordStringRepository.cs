
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Common.Infrastructure.DomainPersistence.EF;
using HC.Isaac.Domain.Aggregate.PosWordString;
using HC.Isaac.Infrastructure.DomainPersistence.Entity;
namespace HC.Isaac.Infrastructure.DomainPersistence.Repository.PosWordString
{
    public class PosWordStringRepository : AggregateEFRepository<PosWordStringAR, PosWordStringPE, PosWordStringDbContext>
    {
        public PosWordStringRepository(IAsyncDbContextFactory<PosWordStringDbContext> factory)
            : base(factory, true)
        {
        }

        protected override IQueryable<PosWordStringPE> PredicateForFullyLoadedAggregate(IQueryable<PosWordStringPE> query)
        {
            return query;
        }       

        public async Task<bool> ExistsAsync(TenantUniqueId tenantUniqueId, UniqueId uniqueId)
        {
            Condition.Requires(tenantUniqueId, nameof(tenantUniqueId))
                .IsNotNull();
            Condition.Requires(uniqueId, nameof(uniqueId))
                .IsNotNull();

            return await base.AnyAsync(Loading.Sober, tenantUniqueId, f => f.UniqueId == uniqueId.Value);
        }       
    }
}
