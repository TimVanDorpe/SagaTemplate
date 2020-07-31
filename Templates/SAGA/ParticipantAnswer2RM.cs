        
using HC.Common;
using System;
namespace HC.Answer.Processor.Saga.ReplyMessage
{
    public class ParticipantAnswer2RM : HC.ReplyMessage
    {
        public ParticipantAnswer2RM(
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
