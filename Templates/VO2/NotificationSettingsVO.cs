using System;
using System.Collections.Generic;
using System.Linq;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using HC.ImportExport.Domain.Aggregate.Shared;
using Newtonsoft.Json;

namespace HC.ImportExport.Domain.Aggregate.NotificationSettings
{
    [Serializable]
    public class NotificationSettingsVO : ComparableValueObject
    {
        [JsonConstructor]
        public NotificationSettingsVO(
		    bool receiveSuccesEmails , 
		    bool receiveFailureEmails , 
            UniqueId uniqueId = null,
            PersistenceState state = PersistenceState.Added
            )
            : base(state)
        {
            
		    this.ReceiveSuccesEmails = receiveSuccesEmails;
		    this.ReceiveFailureEmails = receiveFailureEmails;
            UniqueId = uniqueId == null ? UniqueId.Generate() : uniqueId;
       
        }
		
		public bool ReceiveSuccesEmails { get; private set;}
		
		public bool ReceiveFailureEmails { get; private set;}
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
		 public static NotificationSettingsVO Create(UniqueId uniqueId,bool receiveSuccesEmails,bool receiveFailureEmails)
        {
            return new NotificationSettingsVO(receiveSuccesEmails,receiveFailureEmails, uniqueId);
        }
    }
}

