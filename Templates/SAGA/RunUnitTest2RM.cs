        
using HC.Common;
using System;
namespace HC.Swatson.Processor.Saga.ReplyMessage
{
    public class RunUnitTest2RM : HC.ReplyMessage
    {
        public RunUnitTest2RM(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,
            UniqueId uniqueId
            )
            :base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public UniqueId UniqueId { get; set; }
    }
}
