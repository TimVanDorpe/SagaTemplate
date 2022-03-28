        
  
using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.Command;
using HC.Answer.Processor.Saga.ReplyMessage;
namespace HC.Answer.Processor
{
    public static class PredictAnswerMapper
    {
      public static PredictAnswer1CMD MapToCommand(this PredictAnswerCMD command)
        {
            return new PredictAnswer1CMD(
                tenantUniqueId: command.TenantUniqueId,
                correlationUniqueId: command.CorrelationUniqueId,
                userUniqueId: command.UserUniqueId                
            );
        }

  
        public static PredictAnswer2CMD MapToCommand(this PredictAnswer1RM message)
        {
            return new PredictAnswer2CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId                
            );
        }
  

        public static PredictAnswer1RM MapToReplyMessage(this PredictAnswer1CMD command)
        {
            return new PredictAnswer1RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId
            );
        }
        public static PredictAnswer2RM MapToReplyMessage(this PredictAnswer2CMD command)
        {
            return new PredictAnswer2RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId
            );
        }
  

     }
}
