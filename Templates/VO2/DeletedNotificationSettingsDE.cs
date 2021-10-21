using System;
using HC.Common;
using Newtonsoft.Json;

namespace HC.ImportExport.Domain.Aggregate.FileImportSettings
{
    [Serializable]
    public class DeletedNotificationSettingsDE : DomainEvent
    {
        [JsonConstructor]
        public DeletedNotificationSettingsDE(TenantUniqueId tenantUniqueId, UniqueId uniqueId, UniqueId notificationSettingsUniqueId) 
            : base(tenantUniqueId, uniqueId)
        {
            this.NotificationSettingsUniqueId = notificationSettingsUniqueId;
        }
        public UniqueId NotificationSettingsUniqueId { get; set; }
    }
}
