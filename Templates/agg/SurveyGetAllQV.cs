
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using HC.Common;
using HC.LegacySync.Application.Models.ViewModel.Survey;

namespace HC.LegacySync.Application.Query
{
    public class SurveyGetAllQV : QueryValidator<SurveyGetAllQRY, IEnumerable<SurveyVM>>
    {
        public SurveyGetAllQV(
            ILogger logger
            )
            : base(logger)
        {
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

        protected async override Task<Result<IEnumerable<SurveyVM>>> ExecuteAsync(SurveyGetAllQRY query, string ruleSet = null)
        {
            // Conditions
            Condition.Requires(query, nameof(query)).IsNotNull();

            // Log line
            Log.Debug("Validate query", query.CorrelationUniqueId.ToUniqueId(), query);

            // init result
            var result = new Result<IEnumerable<SurveyVM>>(await base.ValidateAsync(query));

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                Log.Debug("Fluent validation results are valid", query.CorrelationUniqueId.ToUniqueId());
            }
            else
            {
                // Log line
                Log.ValidationError("Fluent validation results are invalid", query.CorrelationUniqueId.ToUniqueId(), result);
            }

            // Log line
            Log.Debug("Validation is done", query.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
