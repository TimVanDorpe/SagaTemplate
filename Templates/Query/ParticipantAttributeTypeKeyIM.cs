  using System;

namespace HC.Core.Application.Models.InputModel.ParticipantAttributeTypeKey
{
    [Serializable]
    public class ParticipantAttributeTypeKeyIM  : IInputModel
    {
      public Guid CorrelationUniqueId { get; set; }

      public Guid TenantUniqueId { get; set; }

           
    }
}
