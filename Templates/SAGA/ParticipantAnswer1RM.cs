using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class ParticipantAnswer1RM : HC.ReplyMessage
    {
        public ParticipantAnswer1RM(
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
