using System;
using HC.Common;
using Newtonsoft.Json;

namespace HC.ImportExport.Domain.Aggregate.FileImportSettings
{

    [Serializable]
    public class AddedNotificationSettingsDE : DomainEvent
    {
        [JsonConstructor]
		public AddedNotificationSettingsDE(TenantUniqueId tenantUniqueId, UniqueId uniqueId, UniqueId notificationSettingsUniqueId,bool receiveSuccesEmails,bool receiveFailureEmails           ) 
            : base(tenantUniqueId, uniqueId)
        {
            notificationSettingsUniqueId.Requires().IsNotNull();

            NotificationSettingsUniqueId = notificationSettingsUniqueId;    
		    this.ReceiveSuccesEmails = receiveSuccesEmails ;
		    this.ReceiveFailureEmails = receiveFailureEmails ;
        }

        public UniqueId NotificationSettingsUniqueId { get; set; }      
		public bool ReceiveSuccesEmails {get;set;}
		public bool ReceiveFailureEmails {get;set;}
 
    }
}


