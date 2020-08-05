using HC.Common;
using System;

namespace HC.Swatson.Application.Command
{
    [Serializable]
    public class AddSectorToTouchpointCMD : HC.Command
    {
        public AddSectorToTouchpointCMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,            
            UniqueId touchpointUniqueId,             
            UniqueId sectorUniqueId)
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
		   this.TouchpointUniqueId = touchpointUniqueId; 
		   this.SectorUniqueId = sectorUniqueId; 
        }
		public UniqueId TouchpointUniqueId { get; set; }
		public UniqueId SectorUniqueId { get; set; }
    }
}      
