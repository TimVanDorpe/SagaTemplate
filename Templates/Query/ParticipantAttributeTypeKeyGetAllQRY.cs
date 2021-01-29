using System;
using HC.Core.Application.Models.InputModel.ParticipantAttributeTypeKey;

namespace HC.Core.Application.Query
{
    public class ParticipantAttributeTypeKeyGetAllQRY : HC.Query
    {
        public ParticipantAttributeTypeKeyGetAllQRY(
            Guid tenantUniqueId, 
            Guid correlationUniqueId, 
            Guid userUniqueId,
            ParticipantAttributeTypeKeyIM model
            ) 
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
            ParticipantAttributeTypeKeyIM = model;
        }

        public ParticipantAttributeTypeKeyIM ParticipantAttributeTypeKeyIM { get; set; }
    }
}
