        
using HC.Common;
using System;
namespace HC.Swatson.Processor.Saga.ReplyMessage
{
    public class RunUnitTest3RM : HC.ReplyMessage
    {
        public RunUnitTest3RM(
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
