  
using System.Threading.Tasks;
using HC.Common;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace HC.LegacySync.Application.Command
{
    public class DeleteASurveyCV : CommandValidator<DeleteASurveyCMD>
    {
        private readonly SurveyRepository repository;

        public DeleteASurveyCV(
            ILogger logger,
            SurveyRepository repository
            )
            : base(logger)
        {
            // Conditions
            Condition.Requires(repository, nameof(repository)).IsNotNull();
            repository = repository;

			// Validation rules
			RuleFor(x => x.Version)
                .NotNull()
                .NotEqual(0)
                .WithSeverity(Severity.Error);

            RuleFor(x => x.TenantUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.UserUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.CorrelationUniqueId)
               .NotNull()
                .NotEqual(new Guid());
        }
        public override async Task<Result> ExecuteAsync(DeleteASurveyCMD command, string ruleSet = null)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();

            // Log line
            Log.Debug("Validate command", command.CorrelationUniqueId.ToUniqueId(), command);

            // init result
            var result = new Result(await base.ValidateAsync(command), command.CorrelationUniqueId.ToUniqueId());

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                Log.Debug("Fluent validation results are valid", command.CorrelationUniqueId.ToUniqueId());

                 // Get aggregate from persistence
                var aggregate = await repository.FindAsync(Loading.Full, command.TenantUniqueId.ToTenantUniqueId(), command.UniqueId.ToUniqueId());

                if (aggregate == null)
                {
                    // Log line
                    Log.ValidationError("Can't find this Survey in the persistence.", command.CorrelationUniqueId.ToUniqueId(), command);

                    // Add property failure to result object
                    result.AddPropertyFailure("uniqueId", "Can't find this Survey in the persistence. Please re-check the conditions.");
                }
            }
            else
            {
                // Log line
                Log.ValidationError("Fluent validation results are invalid", command.CorrelationUniqueId.ToUniqueId(), result);
            }

            // Log line
            Log.Debug("Validation is done", command.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
