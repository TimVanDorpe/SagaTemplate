
using System;
using System.Collections.Generic;
using System.Text;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using Newtonsoft.Json;

namespace HC.Isaac.Application.Command
{
    [Serializable]
    public class UpdateAAliasCMD : HC.Command
    {
        [JsonConstructor]
        public UpdateAAliasCMD(
            // Default props
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,
            long version

            // Custom props
		     , Text text
		     , UniqueId frameworkUniqueId
		     , UniqueId categoryUniqueId
			)
            : base(tenantUniqueId, correlationUniqueId, userUniqueId, version)
        {
		   this.Text = text; 
		   this.FrameworkUniqueId = frameworkUniqueId; 
		   this.CategoryUniqueId = categoryUniqueId; 
        }
		public Text Text { get; set; }
		public UniqueId FrameworkUniqueId { get; set; }
		public UniqueId CategoryUniqueId { get; set; }
    }
}

