using HC.Common;
using System;

namespace HC.Swatson.Processor.Saga.Command
{
    [Serializable]
    public class RunUnitTest1CMD : HC.Command
    {
        public RunUnitTest1CMD(
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
      
