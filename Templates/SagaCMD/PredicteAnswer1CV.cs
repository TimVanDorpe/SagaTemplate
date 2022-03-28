   
using System.Threading.Tasks;
using HC.Common;
using HC.Common.Processor.Logging;
using FluentValidation;
using System;
using HC.Answer.Infrastructure.DomainPersistence.Repository;
using FluentValidation.Results;
using HC.Common.Processor.NSB;
using HC.Answer.Processor.Saga.State;

namespace HC.Answer.Processor.Saga.Command
{
    public class PredicteAnswer1CV : NsbMessageValidator<PredicteAnswer1CMD>
    {
        private readonly TenantRepository _repository;

        public PredicteAnswer1CV(
            IProcessLogger logger,
            TenantRepository repository,
             AppSettings appSettings
            )
            :base(logger , appSettings)
        {
            // Conditions
            Condition.Requires(repository, nameof(repository)).IsNotNull();

            // Init
            _repository = repository;

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
        }

        public override async Task<Result> ExecuteAsync(PredicteAnswer1CMD message)
        {
            // Conditions
            Condition.Requires(message, nameof(message)).IsNotNull();

            // Log line
            var sw = this.Log.MessageValidatorStart( message.UserUniqueId.ToUniqueId(), message.CorrelationUniqueId.ToUniqueId(), message);

            // init results
            var result = new Result(await base.ValidateAsync(message), message.CorrelationUniqueId.ToUniqueId());

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                this.Log.Debug("Fluent validation results are valid",  message.CorrelationUniqueId.ToUniqueId());

                 // Check if the unique id isn't in the persistence yet
                if (await _repository.ExistsAsync(message.TenantUniqueId.ToTenantUniqueId(), UniqueId.Create(message.UniqueId)))
                {
                    // Log line
                    Log.ValidationError("Tenant already exist in the persistence.", message.CorrelationUniqueId.ToUniqueId());

                    // Add property failure to result object
                    result.AddPropertyFailure(new ValidationFailure("uniqueId", "Tenant already exist in the persistence"));
                }
            }
            else
            {
                // Log line
                this.Log.ValidationError("Fluent validation results are invalid", message.CorrelationUniqueId.ToUniqueId(), result);
            }

            // Log line
            this.Log.MessageValidatorFinish(sw, message.CorrelationUniqueId.ToUniqueId(), message);

            // Return result
            return result;
        }

         public override AsyncPollingInfo ConfigureAsyncPolling()
        {
            return AsyncPollingInfo.Create(
                 step: 1,
                 total: PredicteAnswerState.Total,
                 message: PredicteAnswerState.Step1);
        }
    }
}

  