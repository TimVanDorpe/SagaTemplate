using HC.Common;
using System;

namespace HC.Answer.Processor.Saga.Command
{
    [Serializable]
    public class ParticipantAnswer1CMD : HC.Command
    {
        public ParticipantAnswer1CMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,
            UniqueId uniqueId           
            )
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            this.UniqueId = uniqueId;           
        }
        public UniqueId UniqueId { get; set; }     
    }
}
      