
// PROPERTIES
        private readonly List<NarcisticConditionVO> _environmentConditions = new List<NarcisticConditionVO>();
        private readonly List<NarcisticConditionVO> _persistenceEnvironmentConditions = new List<NarcisticConditionVO>();
        public ReadOnlyCollection<NarcisticConditionVO> EnvironmentConditions => _environmentConditions.AsReadOnly();
        public ReadOnlyCollection<NarcisticConditionVO> PersistenceEnvironmentConditions => _persistenceEnvironmentConditions.AsReadOnly();

// Public methods
		  public void AddEnvironmentConditions(NarcisticConditionVO environmentConditions)
        {
            // Conditions
            CanAddEnvironmentConditions(environmentConditions).Requires(nameof(CanAddEnvironmentConditions)).IsTrue();

            // Apply Event
            Domain_ApplyEvent(new AddedNarcisticConditionDE(TenantUniqueId, UniqueId, environmentConditions.UniqueId));
        }
        public void RemoveAEnvironmentConditions(UniqueId environmentConditionsUniqueId)
        {
            // Conditions
            CanDeleteEnvironmentConditions(environmentConditionsUniqueId).Requires(nameof(CanDeleteEnvironmentConditions)).IsTrue();

            // Apply Event
            Domain_ApplyEvent(new DeletedNarcisticConditionDE(TenantUniqueId, UniqueId, environmentConditionsUniqueId));
        }
// validation methods
	    public bool CanDeleteEnvironmentConditions(UniqueId environmentConditionsUniqueId)
        {
            return EnvironmentConditions.Any(s => s.UniqueId == environmentConditionsUniqueId);
        }
		  public bool CanAddEnvironmentConditions(NarcisticConditionVO environmentConditions)
        {
            if (EnvironmentConditions.Any(s => s == environmentConditions))
                return false;
            return true;
        }
// handle
           private void Handle(AddedNarcisticConditionDE e)
        {
            var narcisticCondition = NarcisticConditionVO.Create(e.NarcisticConditionUniqueId ,e.Noiseless,e.Pos);

            _narcisticConditions.Add(narcisticCondition);
            _persistenceNarcisticConditions.Add(narcisticCondition);
        }
		        private void Handle(DeletedNarcisticConditionDE e)
        {
            var narcisticCondition = _narcisticConditions.Single(s => s.UniqueId == e.NarcisticConditionUniqueId);
            _narcisticConditions.Remove(narcisticCondition);
            _persistenceNarcisticConditions.First(x => x.UniqueId == e.NarcisticConditionUniqueId).Persistence_MarkAsDeleted();
        }



