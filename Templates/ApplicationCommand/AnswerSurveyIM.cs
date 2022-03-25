using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Answer.Application.Models.InputModel
{
    [Serializable]
    public class AnswerSurveyIM : IInputModel
    {
        public Guid CorrelationUniqueId { get; set; }
        public Guid TenantUniqueId { get; set; }
		public AnswerSurveyIM AnswerSurvey { get; set; }
       
    }
}


  