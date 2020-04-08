
using System;
using System.Collections.Generic;
using System.Text;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using Newtonsoft.Json;

namespace HC.Isaac.Application.Command
{
    [Serializable]
    public class UpdateAAnnotatedWordStringCMD : HC.Command
    {
        [JsonConstructor]
        public UpdateAAnnotatedWordStringCMD(
            // Default props
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,
            long version

            // Custom props
		     , UniqueId languageUniqueId
		     , Text text
		     , UniqueId defaultCaa
			)
            : base(tenantUniqueId, correlationUniqueId, userUniqueId, version)
        {
		   this.LanguageUniqueId = languageUniqueId; 
		   this.Text = text; 
		   this.DefaultCaa = defaultCaa; 
        }
		public UniqueId LanguageUniqueId { get; set; }
		public Text Text { get; set; }
		public UniqueId DefaultCaa { get; set; }
    }
}

