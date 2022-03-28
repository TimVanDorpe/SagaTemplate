
using HC.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.LegacySync.Domain.Aggregate.Survey.DE
{
    [Serializable]
    public class DeletedASurveyDE : DomainEvent
    {
        [JsonConstructor]
        public DeletedASurveyDE(TenantUniqueId tenantId, UniqueId id, long? version = null)
            : base(tenantId, id, version)
        {
            TenantUniqueId = tenantId;
            UniqueId = id;
        }
    }
}
