using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class PredictAnswer1RM : HC.ReplyMessage
    {
        public PredictAnswer1RM(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId            
            )
            :base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            
        }        
    }
}
