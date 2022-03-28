using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HC.LegacySync.Application.Models.InputModel.UpdateASurvey
{
    [Serializable]
    public class UpdateASurveyIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
		public Guid UniqueId { get; set; }		
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }
		public Guid TouchpointUniqueId { get; set; }
    
    }
}
