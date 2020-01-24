using HC.Common;
using HC.Isaac.Application.Command;
using System;
using System.Threading.Tasks;

namespace HC.Isaac.Application.Command
{
    public class CreatePosWordStringCH : CommandHandler<CreatePosWordStringCMD, PosWordStringAR>
    {
        public CreateAPosWordStringCH(PosWordStringRepository repository)
            : base(repository)
        {

        }

        protected override async Task ExecuteAsync(CreatePosWordStringCMD command)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            this.Log.Debug("Handle command", command.CorrelationUniqueId.ToUniqueId(), command);

			// Get the aggregate from the persistence
            var aggregate = await this.Repository.FindAsync(Loading.Full, command.TenantUniqueId.ToTenantUniqueId(),
                UniqueId.Generate());
			
			 // Save aggregate to DB
            await this.Repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate);

            
            // Log line
            this.Log.Debug("Handling command is done", command.CorrelationUniqueId.ToUniqueId());
        }
    }
}
