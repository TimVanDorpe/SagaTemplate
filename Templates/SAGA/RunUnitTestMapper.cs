        
  
using HC.Swatson.Application.Command;
using HC.Swatson.Processor.Saga.Command;
using HC.Swatson.Processor.Saga.ReplyMessage;
namespace HC.Swatson.Processor
{
    public static class RunUnitTestMapper
    {
        public static RunUnitTest1RM MapToReplyMessage(this RunUnitTest1CMD command)
        {
            return new RunUnitTest1RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
        public static RunUnitTest2RM MapToReplyMessage(this RunUnitTest2CMD command)
        {
            return new RunUnitTest2RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
        public static RunUnitTest3RM MapToReplyMessage(this RunUnitTest3CMD command)
        {
            return new RunUnitTest3RM(
               tenantUniqueId: command.TenantUniqueId,
               correlationUniqueId: command.CorrelationUniqueId,
               userUniqueId: command.UserUniqueId          
            );
        }
  

 
        public static RunUnitTest1CMD MapToCommand(this RunUnitTest1RM message)
        {
            return new RunUnitTest1CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
 
        public static RunUnitTest2CMD MapToCommand(this RunUnitTest2RM message)
        {
            return new RunUnitTest2CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
 
        public static RunUnitTest3CMD MapToCommand(this RunUnitTest3RM message)
        {
            return new RunUnitTest3CMD(
                tenantUniqueId: message.TenantUniqueId,
                correlationUniqueId: message.CorrelationUniqueId,
                userUniqueId: message.UserUniqueId             
            );
        }
  
     }
}
