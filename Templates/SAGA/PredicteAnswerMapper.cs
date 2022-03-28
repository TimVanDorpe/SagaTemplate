        
  
using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.Command;
using HC.Answer.Processor.Saga.ReplyMessage;
namespace HC.Answer.Processor
{
    public static class PredicteAnswerMapper
    {
      public static PredicteAnswer1CMD MapToCommand(this PredicteAnswerCMD command)
        {
            return new PredicteAnswer1CMD(
                tenantUniqueId: command.TenantUniqueId,
                correlationUniqueId: command.CorrelationUniqueId,
                userUniqueId: command.UserUniqueId                
            );
        }

  
        public static PredicteAnswer2CMD MapToCommand(this PredicteAnswer1RM message)
        {
            return new PredicteAnswer2CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId                
            );
        }
  

        public static PredicteAnswer1RM MapToReplyMessage(this PredicteAnswer1CMD command)
        {
            return new PredicteAnswer1RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId
            );
        }
        public static PredicteAnswer2RM MapToReplyMessage(this PredicteAnswer2CMD command)
        {
            return new PredicteAnswer2RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId
            );
        }
  

     }
}
