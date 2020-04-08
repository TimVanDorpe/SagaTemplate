using System;
using HC.Common;
using Newtonsoft.Json;

namespace HC.Isaac.Domain.Aggregate.AnnotatedWordString
{

    [Serializable]
    public class AddedNarcisticConditionDE : DomainEvent
    {
        [JsonConstructor]
		public AddedNarcisticConditionDE(TenantUniqueId tenantUniqueId, UniqueId uniqueId, UniqueId narcisticConditionUniqueId,bool noiseless,PosVO pos           ) 
            : base(tenantUniqueId, uniqueId)
        {
            narcisticConditionUniqueId.Requires().IsNotNull();

            NarcisticConditionUniqueId = narcisticConditionUniqueId;    
		    this.Noiseless = noiseless ;
		    this.Pos = pos ;
        }

        public UniqueId NarcisticConditionUniqueId { get; set; }      
		public bool Noiseless {get;set;}
		public PosVO Pos {get;set;}
 
    }
}


