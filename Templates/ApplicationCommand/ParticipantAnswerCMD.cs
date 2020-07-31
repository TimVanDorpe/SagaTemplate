using HC.Common;
using System;

namespace HC.Answer.Application.Command
{
    [Serializable]
    public class ParticipantAnswerCMD : HC.Command
    {
        public ParticipantAnswerCMD(
            Guid tenantUniqueId,
            Guid correlationUniqueId,
            Guid userUniqueId,            
            Text text,             
            UniqueId frameworkUniqueId,             
            UniqueId categoryUniqueId)
            : base(tenantUniqueId, correlationUniqueId, userUniqueId)
        {
		   this.Text = text; 
		   this.FrameworkUniqueId = frameworkUniqueId; 
		   this.CategoryUniqueId = categoryUniqueId; 
        }
		public Text Text { get; set; }
		public UniqueId FrameworkUniqueId { get; set; }
		public UniqueId CategoryUniqueId { get; set; }
    }
}      
