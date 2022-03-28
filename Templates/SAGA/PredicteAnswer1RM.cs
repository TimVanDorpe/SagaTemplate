using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class PredicteAnswer1RM : HC.ReplyMessage
    {
        public PredicteAnswer1RM(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId            
            )
            :base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            
        }        
    }
}
