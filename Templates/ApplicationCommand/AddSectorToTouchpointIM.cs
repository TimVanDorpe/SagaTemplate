using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Swatson.Application.Models.InputModel.Touchpoint
{
    [Serializable]
    public class AddSectorToTouchpointIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
		public UniqueId TouchpointUniqueId { get; set; }
		public UniqueId SectorUniqueId { get; set; }
       
    }
}


  