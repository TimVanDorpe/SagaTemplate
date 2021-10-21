
// PROPERTIES
        private readonly List<NotificationSettingsVO> _notificationSettings = new List<NotificationSettingsVO>();
        private readonly List<NotificationSettingsVO> _persistenceNotificationSettings = new List<NotificationSettingsVO>();
        public ReadOnlyCollection<NotificationSettingsVO> NotificationSettings => _notificationSettings.AsReadOnly();
        public ReadOnlyCollection<NotificationSettingsVO> PersistenceNotificationSettings => _persistenceNotificationSettings.AsReadOnly();

// Public methods
		  public void AddNotificationSettings(NotificationSettingsVO notificationSettings)
        {
            // Conditions
            CanAddNotificationSettings(notificationSettings).Requires(nameof(CanAddNotificationSettings)).IsTrue();

            // Apply Event
            Domain_ApplyEvent(new AddedNotificationSettingsDE(TenantUniqueId, UniqueId, notificationSettings.UniqueId));
        }
        public void RemoveANotificationSettings(UniqueId notificationSettingsUniqueId)
        {
            // Conditions
            CanDeleteNotificationSettings(notificationSettingsUniqueId).Requires(nameof(CanDeleteNotificationSettings)).IsTrue();

            // Apply Event
            Domain_ApplyEvent(new DeletedNotificationSettingsDE(TenantUniqueId, UniqueId, notificationSettingsUniqueId));
        }
// validation methods
	    public bool CanDeleteNotificationSettings(UniqueId notificationSettingsUniqueId)
        {
            return NotificationSettings.Any(s => s.UniqueId == notificationSettingsUniqueId);
        }
		  public bool CanAddNotificationSettings(NotificationSettingsVO notificationSettings)
        {
            if (NotificationSettings.Any(s => s == notificationSettings))
                return false;
            return true;
        }
// handle
           private void Handle(AddedNotificationSettingsDE e)
        {
            var notificationSettings = NotificationSettingsVO.Create(e.NotificationSettingsUniqueId ,e.ReceiveSuccesEmails,e.ReceiveFailureEmails);

            _notificationSettingss.Add(notificationSettings);
            _persistenceNotificationSettingss.Add(notificationSettings);
        }
		        private void Handle(DeletedNotificationSettingsDE e)
        {
            var notificationSettings = _notificationSettingss.Single(s => s.UniqueId == e.NotificationSettingsUniqueId);
            _notificationSettingss.Remove(notificationSettings);
            _persistenceNotificationSettingss.First(x => x.UniqueId == e.NotificationSettingsUniqueId).Persistence_MarkAsDeleted();
        }



