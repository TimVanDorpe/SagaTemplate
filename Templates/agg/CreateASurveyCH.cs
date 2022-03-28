using HC.Common;
using HC.LegacySync.Application.Command;
using System;
using System.Threading.Tasks;

namespace HC.LegacySync.Application.Command
{
    public class CreateASurveyCH : CommandHandler<CreateASurveyCMD, SurveyAR>
    {
        public CreateASurveyCH(SurveyRepository repository)
            : base(repository)
        {

        }

        protected override async Task ExecuteAsync(CreateASurveyCMD command)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();
            command.UserUniqueId.Requires(nameof(command.UserUniqueId)).IsNotEqualTo(new Guid());
            command.TenantUniqueId.Requires(nameof(command.TenantUniqueId)).IsNotEqualTo(new Guid());
            command.CorrelationUniqueId.Requires(nameof(command.CorrelationUniqueId)).IsNotEqualTo(new Guid());

            // Log line
            Log.Debug("Handle command", command.CorrelationUniqueId.ToUniqueId(), command);

			// Create a new aggregate
            var aggregate = SurveyAR.Create(command.TenantUniqueId.ToTenantUniqueId(), UniqueId.Generate()
, command.TouchpointUniqueId);
			
			 // Save aggregate to DB
            await Repository.SaveAsync(command.CorrelationUniqueId.ToUniqueId(), command.UserUniqueId.ToUniqueId(), aggregate);

            
            // Log line
            Log.Debug("Handling command is done", command.CorrelationUniqueId.ToUniqueId());
        }
    }
}
