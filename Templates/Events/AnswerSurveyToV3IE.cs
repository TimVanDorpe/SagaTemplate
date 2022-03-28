using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Integration.Events.LegacySync
{
   public class AnswerSurveyToV3IE : Event
    {
        [JsonConstructor]
        public AnswerSurveyToV3IE(
           Guid tenantUniqueId,
           Guid correlationUniqueId,
           Guid userUniqueId,
           
           Guid uniqueId,
            
            string name,            
            string description 
        )
           : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            this.UniqueId = uniqueId;
		    this.Name = name;
		    this.Description = description;
        }

        public Guid UniqueId { get; set; }
		public string Name{ get; set;} 
		public string Description{ get; set;} 
    }
}

