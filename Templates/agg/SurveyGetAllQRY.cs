using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HC.LegacySync.Application.Query
{
    [Serializable]
    public class SurveyGetAllQRY : HC.Query
    {
		[JsonConstructor]
        public SurveyGetAllQRY(
            // Default props
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId
        )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
        }
    }
}
