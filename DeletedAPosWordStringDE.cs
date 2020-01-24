
using HC.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Isaac.Domain.Aggregate.PosWordString.DE
{
    [Serializable]
    public class DeletedAPosWordStringDE : DomainEvent
    {
        [JsonConstructor]
        public DeletedAPosWordStringDE(TenantUniqueId tenantId, UniqueId id, long? version = null)
            : base(tenantId, id, version)
        {
            this.TenantUniqueId = tenantId;
            this.UniqueId = id;
        }
    }
}
