using HC.Common;
using System;

namespace HC.Answer.Processor.Saga.Command
{
    [Serializable]
    public class PredicteAnswer1CMD : HC.Command
    {
        public PredicteAnswer1CMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId                 
            )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {                    
        }       
    }
}      
