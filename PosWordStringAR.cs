

// Aggregate
using System;
using System.Collections.Generic;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using HC.Isaac.Domain.Aggregate.PosWordString.DE;

namespace HC.Isaac.Domain.Aggregate.PosWordString
{
    [Serializable]
    public class PosWordStringAR : AggregateRoot<PosWordStringAR>
    {
        #region Constructors
       public PosWordStringAR(TenantUniqueId tenantUniqueId 
	   	   , Text text
			   , LanguageVO language
			   , PosPolarityENUM defaultpospolarity
			   , UniqueId uniqueId = null
       , PersistenceState state = PersistenceState.Unchanged
	   , long version = 1)
            : base(tenantUniqueId, uniqueId, state, version)
        {
            tenantUniqueId.Requires(nameof(tenantUniqueId))
                .IsNotNull();

            uniqueId.Requires(nameof(uniqueId))
                .IsNotNull();
         
		 // Check if anything needs to be required

          state.Requires()
                .Evaluate(x => x != PersistenceState.SoberLoadedFromPersistence, "This aggregate doesn't support the sober loading from persistence");

            Domain_ApplyEvent(new CreatedAPosWordStringDE(
			this.TenantUniqueId
			,this.UniqueId
			,text
			,language
			,defaultpospolarity
			));
        }
        #endregion
        
        #region Properties
				public Text Text {get;set;}
				public LanguageVO Language {get;set;}
				public PosPolarityENUM DefaultPosPolarity {get;set;}
		
        #endregion Properties

        #region Public methods

        public PosWordStringAR Delete(long? version = null)
        {
            // Conditions
            Condition.Requires(this.CanDelete(), nameof(CanDelete)).IsTrue();

            // Init
            if (version.HasValue)
                this.Version = version.Value;
            Domain_ApplyEvent(new DeletedAPosWordStringDE(this.TenantUniqueId, this.UniqueId));

            return this;
        }       


        #endregion


        #region Handlers

        private void Handle(CreatedAPosWordStringDE evt)
        {
           			this.Text = evt.Text;
		   			this.Language = evt.Language;
		   			this.DefaultPosPolarity = evt.DefaultPosPolarity;
		           }      
        private void Handle(DeletedAPosWordStringDE evnt)
        {
            this.Persistence_MarkAsDeleted();
        }
        #endregion

        #region Validation methods

        public bool CanDelete()
        {
           // Add validation to delete !
		   // yield return this.Text; 
        }
    
        #endregion

        #region Public Overrides

        public override IEnumerable<ComparableValueObject> Domain_IdentityComponents()
        {
            //Needs to be filled in !
        }

        #endregion

        public static PosWordStringAR Create(
		TenantUniqueId tenantUniqueId
		,UniqueId uniqueId
		,Text text
		,LanguageVO language
		,PosPolarityENUM defaultpospolarity
		)
        {
            return new PosWordStringAR(tenantUniqueId 
		,text
		,language
		,defaultpospolarity
		,uniqueId
		,PersistenceState.Added);
        }


    }
}

