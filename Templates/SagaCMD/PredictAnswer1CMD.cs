using HC.Common;
using System;

namespace HC.Answer.Processor.Saga.Command
{
    [Serializable]
    public class PredictAnswer1CMD : HC.Command
    {
        public PredictAnswer1CMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId                 
            )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {                    
        }       
    }
}      
