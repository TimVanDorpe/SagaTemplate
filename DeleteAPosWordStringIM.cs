

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Isaac.Application.Models.InputModel.DeleteAPosWordString
{
    [Serializable]
    public class DeleteAPosWordStringIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
       
        public Guid UniqueId { get; set; }
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }
    }
}
