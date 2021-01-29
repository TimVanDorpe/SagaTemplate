using System;
using System.Threading.Tasks;
using FluentValidation;
using HC.Common;
using HC.Common.Infrastructure.DomainPersistence;
using HC.Core.Application.Models.ViewModel.ParticipantAttributeTypeKey;
using HC.Core.Infrastructure.DomainPersistence.Repository.ParticipantAttributeTypeKey;

namespace HC.Core.Application.Query
{
    public class ParticipantAttributeTypeKeyGetAllQV : QueryValidator<ParticipantAttributeTypeKeyGetAllQRY, ParticipantAttributeTypeKeyVM>
    {
		private readonly ParticipantAttributeTypeKeyRepository _participantAttributeTypeKeyRepository;

        public ParticipantAttributeTypeKeyGetAllQV(ILogger logger , ParticipantAttributeTypeKeyRepository participantAttributeTypeKeyRepository) : base(logger)
        {
			participantAttributeTypeKeyRepository.Requires().IsNotNull();
            _participantAttributeTypeKeyRepository = participantAttributeTypeKeyRepository;

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

        protected override async Task<Result<ParticipantAttributeTypeKeyVM>> ExecuteAsync(ParticipantAttributeTypeKeyGetAllQRY query, string ruleSet = null)
        {
            // Conditions
            Condition.Requires(query, nameof(query)).IsNotNull();

            // Log line
            this.Log.Debug("Validate query", query.CorrelationUniqueId.ToUniqueId(), query);

            // init result
            var result = new Result<ParticipantAttributeTypeKeyVM>(await base.ValidateAsync(query));

            // If fluent validation properties are valid
            if (result.IsValid)
            {
                // Log line
                this.Log.Debug("Fluent validation results are valid", query.CorrelationUniqueId.ToUniqueId());

				if(await this._participantAttributeTypeKeyRepository.FindAsync(Loading.Full, query.TenantUniqueId.ToTenantUniqueId(), query.UniqueId.ToUniqueId())
                == null)
                {
                    this.Log.ValidationError("UniqueId does not exist", query.CorrelationUniqueId.ToUniqueId(), query);

                    result.AddPropertyFailure("UniqueId", "This uniqueId doesn't exists");
                }

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
