        
using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class PredicteAnswer2RM : HC.ReplyMessage
    {
        public PredicteAnswer2RM(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId            
            )
            :base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            
        }        
    }
}
