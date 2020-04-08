using HC.Common;
using System;
namespace HC.Swatson.Processor.Saga.ReplyMessage
{
    public class RunUnitTest1RM : HC.ReplyMessage
    {
        public RunUnitTest1RM(
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
