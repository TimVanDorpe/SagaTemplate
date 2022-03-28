 
using HC.Common;
using System;

namespace HC.Answer.Processor.Saga.Command
{
    [Serializable]
    public class PredictAnswer2CMD : HC.Command
    {
        public PredictAnswer2CMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId                 
            )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {                    
        }       
    }
}      
