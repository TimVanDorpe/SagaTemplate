 
using HC.Common;
using System;

namespace HC.Answer.Processor.Saga.Command
{
    [Serializable]
    public class PredicteAnswer2CMD : HC.Command
    {
        public PredicteAnswer2CMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId                 
            )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {                    
        }       
    }
}      
