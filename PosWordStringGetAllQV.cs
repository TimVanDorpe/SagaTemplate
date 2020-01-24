
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using HC.Common;
using HC.Isaac.Application.Models.ViewModel.PosWordString;

namespace HC.Isaac.Application.Query
{
    public class PosWordStringGetAllQV : QueryValidator<PosWordStringGetAllQRY, IEnumerable<PosWordStringVM>>
    {
        public PosWordStringGetAllQV(
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

        protected async override Task<Result<IEnumerable<PosWordStringVM>>> ExecuteAsync(PosWordStringGetAllQRY query, string ruleSet = null)
        {
            // Conditions
            Condition.Requires(query, nameof(query)).IsNotNull();

            // Log line
            this.Log.Debug("Validate query", query.CorrelationUniqueId.ToUniqueId(), query);

            // init result
            var result = new Result<IEnumerable<PosWordStringVM>>(await base.ValidateAsync(query));

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                this.Log.Debug("Fluent validation results are valid", query.CorrelationUniqueId.ToUniqueId());
            }
            else
            {
                // Log line
                this.Log.ValidationError("Fluent validation results are invalid", query.CorrelationUniqueId.ToUniqueId(), result);
            }

            // Log line
            this.Log.Debug("Validation is done", query.CorrelationUniqueId.ToUniqueId());

            // Return result
            return result;
        }
    }
}
