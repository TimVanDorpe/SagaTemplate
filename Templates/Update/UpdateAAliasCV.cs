using System.Threading.Tasks;
using HC.Common;
using FluentValidation;
using FluentValidation.Results;
using System;
using HC.Isaac.Infrastructure.DomainPersistence.Repository.Alias;
using HC.Isaac.Application.Command;

namespace HC.Isaac.Application.Command
{
    public class UpdateAAliasCV : CommandValidator<UpdateAAliasCMD>
    {
        private readonly AliasRepository repository;

        public UpdateAAliasCV(
            ILogger logger,
            AliasRepository repository
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

            RuleFor(x => x.UniqueId.Value)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.TenantUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.UserUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.CorrelationUniqueId)
                .NotNull()
                .NotEqual(new Guid());

           // Add for each custom property rules to validate
		     RuleFor(x => x.Text)
                .NotNull();                
		     RuleFor(x => x.FrameworkUniqueId)
                .NotNull();                
		     RuleFor(x => x.CategoryUniqueId)
                .NotNull();                
		   
           

        }
        public override async Task<Result> ExecuteAsync(UpdateAAliasCMD command, string ruleSet = null)
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

				// Validate if we can update the aggregate !
				var aggregate = await this.repository.FindAsync(Loading.Full, command.TenantUniqueId.ToTenantUniqueId(),
                    command.UniqueId);

				if (aggregate == null)
                {
                    // Log line
                    this.Log.ValidationError("UniqueId is wrong, this alias doesn't exist or is disabled", command.CorrelationUniqueId.ToUniqueId(), command);

                    // Add property failure to result object
                    result.AddPropertyFailure(new ValidationFailure("UniqueId", "This  alias doenst exists"));
                }
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
