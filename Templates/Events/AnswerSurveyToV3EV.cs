using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using HC.Common;
using HC.Common.Processor.Logging;
using HC.Common.Processor.NSB;
using HC.Integration;
using HC.Integration.Events.Core;

namespace HC.InsiderMetrics.Processor.Event
{
    public class AnswerSurveyToV3EV : NsbMessageValidator<AnswerSurveyToV3IE>
    {
        private readonly SurveyRepository repository;

        public AnswerSurveyToV3EV(
            IProcessLogger logger,
            SurveyRepository repository
        )
            : base(logger)
        {
            // Conditions
            repository.Requires(nameof(repository)).IsNotNull();
            this.repository = repository;

            // Validation rules

            RuleFor(x => x.TenantUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.UserUniqueId)
                .NotNull()
                .NotEqual(new Guid());

            RuleFor(x => x.CorrelationUniqueId)
                .NotNull()
                .NotEqual(new Guid());          

            RuleFor(x => x.UniqueId)
                .NotNull()
                .NotEqual(new Guid());            
        }

        public override async Task<Result> ExecuteAsync(AnswerSurveyToV3IE message)
        {
            // Conditions
            message.Requires(nameof(message)).IsNotNull();

            // Log line
            var sw = Log.MessageValidatorStart(message.UserUniqueId.ToUniqueId(),
                message.CorrelationUniqueId.ToUniqueId(), message);

            // init results
            var result = new Result(await base.ValidateAsync(message), message.CorrelationUniqueId.ToUniqueId());

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                this.Log.Debug("Fluent validation results are valid", message.CorrelationUniqueId.ToUniqueId());

                // Check if the unique id isn't in the persistence yet
                if (await repository.ExistsAsync(message.TenantUniqueId.ToTenantUniqueId(),
                    message.UniqueId.ToUniqueId()))
                {
                    // Log line
                    this.Log.ValidationError("Touchpoint already exist in the persistence",
                        message.CorrelationUniqueId.ToUniqueId());

                    // Add property failure to result object
                    result.AddPropertyFailure(new ValidationFailure("uniqueId", "The aggregate doesn't exists in the persitence"));
                }
            }
            else
            {
                // Log line
                Log.ValidationError("Fluent validation results are invalid", message.CorrelationUniqueId.ToUniqueId(),
                    result);
            }

            // Log line
            Log.MessageValidatorFinish(sw, message.CorrelationUniqueId.ToUniqueId(), message);

            // Return result
            return result;
        }

        public override AsyncPollingInfo ConfigureAsyncPolling()
        {
            return null;
        }
    }
}
