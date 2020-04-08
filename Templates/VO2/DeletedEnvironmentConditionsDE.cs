using System;
using HC.Common;
using Newtonsoft.Json;

namespace HC.Isaac.Domain.Aggregate.AnnotatedWordString
{
    [Serializable]
    public class DeletedNarcisticConditionDE : DomainEvent
    {
        [JsonConstructor]
        public DeletedNarcisticConditionDE(TenantUniqueId tenantUniqueId, UniqueId uniqueId, UniqueId narcisticConditionUniqueId) 
            : base(tenantUniqueId, uniqueId)
        {
            this.NarcisticConditionUniqueId = narcisticConditionUniqueId;
        }
        public UniqueId NarcisticConditionUniqueId { get; set; }
    }
}
