        
  
using HC.Answer.Application.Command;
using HC.Answer.Processor.Saga.Command;
using HC.Answer.Processor.Saga.ReplyMessage;
namespace HC.Answer.Processor
{
    public static class ParticipantAnswerMapper
    {
        public static ParticipantAnswer1RM MapToReplyMessage(this ParticipantAnswer1CMD command)
        {
            return new ParticipantAnswer1RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
        public static ParticipantAnswer2RM MapToReplyMessage(this ParticipantAnswer2CMD command)
        {
            return new ParticipantAnswer2RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
        public static ParticipantAnswer3RM MapToReplyMessage(this ParticipantAnswer3CMD command)
        {
            return new ParticipantAnswer3RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
  

 
        public static ParticipantAnswer1CMD MapToCommand(this ParticipantAnswer1RM message)
        {
            return new ParticipantAnswer1CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
 
        public static ParticipantAnswer2CMD MapToCommand(this ParticipantAnswer2RM message)
        {
            return new ParticipantAnswer2CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
 
        public static ParticipantAnswer3CMD MapToCommand(this ParticipantAnswer3RM message)
        {
            return new ParticipantAnswer3CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
  
     }
}
