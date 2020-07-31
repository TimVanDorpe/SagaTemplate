        
using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class ParticipantAnswer3RM : HC.ReplyMessage
    {
        public ParticipantAnswer3RM(
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
