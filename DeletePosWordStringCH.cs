using HC.Common;
using HC.Isaac.Application.Command;
using System;
using System.Threading.Tasks;
using HC.Common.Infrastructure.DomainPersistence;
namespace HC.Isaac.Application.Command
{
    public class DeletePosWordStringCH : CommandHandler<DeletePosWordStringCMD, PosWordStringAR>
    {
        public DeleteAPosWordStringCH(PosWordStringRepository repository)
            : base(repository)
        {

        }

        protected override async Task ExecuteAsync(DeletePosWordStringCMD command)
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
                command.UniqueId.ToUniqueId());

			// Delete the aggregate 
            aggregate = aggregate.Delete(command.Version);
			
			 // Save aggregate to DB
            await this.Repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate);

            
            // Log line
            this.Log.Debug("Handling command is done", command.CorrelationUniqueId.ToUniqueId());
        }
    }
}
