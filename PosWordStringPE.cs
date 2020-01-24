
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Integration.Domain.ValueObjects;
using HC.Isaac.Domain.Aggregate.PosWordString;

namespace HC.Isaac.Infrastructure.DomainPersistence.Entity
{
    public class PosWordStringPE : TenantPersistenceEntity, IPersistenceEntityAggregateConvertible<PosWordStringPE, PosWordStringAR>
    {
		public Text Text {get;set;}
		public LanguageVO Language {get;set;}
		public PosPolarityENUM DefaultPosPolarity {get;set;}
       
        
        public Action<PosWordStringPE> MapFromAggregate(PosWordStringAR aggregate)
        {
            return p =>
            {                
                p.State = aggregate.State;
                p.TenantUniqueId = aggregate.TenantUniqueId.Value;
                p.UniqueId = aggregate.UniqueId.Value;
                p.Version = aggregate.Version;
				p.Text = aggregate.Text;
				p.Language = aggregate.Language;
				p.DefaultPosPolarity = aggregate.DefaultPosPolarity;
                
            };
        }

        public Task<PosWordStringAR> MapToAggregate(Loading type)
        {
            var PosWordString = new PosWordStringAR(
                tenantUniqueId: this.TenantUniqueId.ToTenantUniqueId()
				,this.Text
				,this.Language
				,this.DefaultPosPolarity
                ,uniqueId: Common.UniqueId.Create(this.UniqueId)
                ,state: type == Loading.Full
                    ? PersistenceState.FullyLoadedFromPersistence
                    : PersistenceState.SoberLoadedFromPersistence, version: this.Version);

            // Mark as unchanged and return back to sender
            return Task.FromResult(PosWordString.Persistence_MarkAsUnchanged());
        }
    }

}
