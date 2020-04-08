using System;
using System.Collections.Generic;
using System.Linq;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using HC.Isaac.Domain.Aggregate.Shared;
using Newtonsoft.Json;

namespace HC.Isaac.Domain.Aggregate.NarcisticCondition
{
    [Serializable]
    public class NarcisticConditionVO : ComparableValueObject
    {
        [JsonConstructor]
        public NarcisticConditionVO(
		    bool noiseless , 
		    PosVO pos , 
            UniqueId uniqueId = null,
            PersistenceState state = PersistenceState.Added
            )
            : base(state)
        {
            
		    this.Noiseless = noiseless;
		    this.Pos = pos;
            UniqueId = uniqueId == null ? UniqueId.Generate() : uniqueId;
       
        }
		
		public bool Noiseless { get; private set;}
		
		public PosVO Pos { get; private set;}
        public UniqueId UniqueId { get; }

        public void Persistence_MarkAsUnchanged()
        {
            this.State = PersistenceState.Unchanged;
        }
        public void Persistence_MarkAsDeleted()
        {
            this.State = PersistenceState.Deleted;
        }     

        public override IEnumerable<object> Domain_EqualityComponents()
        {
           // TODO : yield return..
        }

        public override IEnumerable<IComparable> Domain_ComparableComponents()
        {
            return Domain_EqualityComponents().Cast<IComparable>();
        }
		 public static NarcisticConditionVO Create(UniqueId uniqueId,bool noiseless,PosVO pos)
        {
            return new NarcisticConditionVO(noiseless,pos, uniqueId);
        }
    }
}

