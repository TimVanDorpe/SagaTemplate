using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Isaac.Application.Query
{
    [Serializable]
    public class PosWordStringGetAllQRY : HC.Query
    {
        public PosWordStringGetAllQRY(
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
