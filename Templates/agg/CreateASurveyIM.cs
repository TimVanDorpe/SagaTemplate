using System;
using System.Collections.Generic;
using System.Text;

namespace HC.LegacySync.Application.Models.InputModel.CreateASurvey
{
    [Serializable]
    public class CreateASurveyIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
		public Guid TouchpointUniqueId { get; set; }
    
    }
}
