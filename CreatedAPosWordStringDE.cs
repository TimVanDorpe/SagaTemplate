// Domain event
using System;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using Newtonsoft.Json;

namespace HC.Isaac.Domain.Aggregate.PosWordString.DE
{
    [Serializable]
    public class CreatedAPosWordStringDE : DomainEvent
    {
        [JsonConstructor]
        public CreatedAPosWordStringDE(TenantUniqueId tenantUniqueId, UniqueId uniqueId
		 		,Text text
				,LanguageVO language
				,PosPolarityENUM defaultpospolarity
				)
            : base(tenantUniqueId, uniqueId)
        {
            Condition.Requires(nameof(tenantUniqueId)).IsNotNull();
            			this.Text = text;
		   			this.Language = language;
		   			this.DefaultPosPolarity = defaultpospolarity;
		           }

       		public Text Text {get;set;}
				public LanguageVO Language {get;set;}
				public PosPolarityENUM DefaultPosPolarity {get;set;}
		    }
}
