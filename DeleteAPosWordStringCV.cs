  
using System.Threading.Tasks;
using HC.Common;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace HC.Isaac.Application.Command
{
    public class DeleteAPosWordStringCV : CommandValidator<DeleteAPosWordStringCMD>
    {
        private readonly PosWordStringRepository repository;

        public DeleteAPosWordStringCV(
            ILogger logger,
            PosWordStringRepository repository
            )
            : base(logger)
        {
            // Conditions
            Condition.Requires(repository, nameof(repository)).IsNotNull();
            this.repository = repository;

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
        public override async Task<Result> ExecuteAsync(DeleteAPosWordStringCMD command, string ruleSet = null)
        {
            // Conditions
            command.Requires(nameof(command)).IsNotNull();

            // Log line
            this.Log.Debug("Validate command", command.CorrelationUniqueId.ToUniqueId(), command);

            // init result
            var result = new Result(await base.ValidateAsync(command), command.CorrelationUniqueId.ToUniqueId());

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                this.Log.Debug("Fluent validation results are valid", command.CorrelationUniqueId.ToUniqueId());
            }
            else
            {
                // Log line
                this.Log.ValidationError("Fluent validation results are invalid", command.CorrelationUniqueId.ToUniqueId(), result);
            }

            // Log line
            this.Log.Debug("Validation is done", command.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
