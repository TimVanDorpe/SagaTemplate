        
using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class PredictAnswer2RM : HC.ReplyMessage
    {
        public PredictAnswer2RM(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId            
            )
            :base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            
        }        
    }
}
