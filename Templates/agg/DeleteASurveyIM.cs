

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.LegacySync.Application.Models.InputModel.Survey
{
    [Serializable]
    public class DeleteASurveyIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
       
        public Guid UniqueId { get; set; }
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }
    }
}
