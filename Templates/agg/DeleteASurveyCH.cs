using HC.Common;
using HC.LegacySync.Application.Command;
using System;
using System.Threading.Tasks;
using HC.Common.Infrastructure.DomainPersistence;
namespace HC.LegacySync.Application.Command
{
    public class DeleteASurveyCH : CommandHandler<DeleteASurveyCMD, SurveyAR>
    {
        public DeleteASurveyCH(SurveyRepository repository)
            : base(repository)
        {

        }

        protected override async Task ExecuteAsync(DeleteASurveyCMD command)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            Log.Debug("Handle command", command.CorrelationUniqueId.ToUniqueId(), command);

			// Get the aggregate from the persistence
            var aggregate = await Repository.FindAsync(Loading.Full, command.TenantUniqueId.ToTenantUniqueId(),
                command.UniqueId.ToUniqueId());

			// Delete the aggregate 
            aggregate = aggregate.Delete(command.Version);
			
			 // Save aggregate to DB
            await Repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate);

            
            // Log line
            Log.Debug("Handling command is done", command.CorrelationUniqueId.ToUniqueId());
        }
    }
}
