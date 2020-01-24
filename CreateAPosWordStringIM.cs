using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Isaac.Application.Models.InputModel.CreateAPosWordString
{
    [Serializable]
    public class CreateAPosWordStringIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
		public Text Text {get;set;}
		public LanguageVO Language {get;set;}
		public PosPolarityENUM DefaultPosPolarity {get;set;}
    
    }
}
