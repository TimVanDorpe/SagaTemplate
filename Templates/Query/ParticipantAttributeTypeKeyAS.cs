using HC.Common;
using HC.Core.Application.Models.ViewModel.ParticipantAttributeTypeKey;
using HC.Core.Application.Models.InputModel.ParticipantAttributeTypeKey;
using HC.Core.Application.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Core.Application
{
    public class ParticipantAttributeTypeKeyAS
    {
    private readonly IQueryProcessor<ParticipantAttributeTypeKeyGetAllQRY, ParticipantAttributeTypeKeyVM> _participantAttributeTypeKeyGetAllProcessor;

     public ParticipantAttributeTypeKeyAS(IQueryProcessor<ParticipantAttributeTypeKeyGetAllQRY, ParticipantAttributeTypeKeyVM> participantAttributeTypeKeyGetAllProcessor)
        {
            _participantAttributeTypeKeyGetAllProcessor = participantAttributeTypeKeyGetAllProcessor;
        }

public async Task<Result<ParticipantAttributeTypeKeyVM>> ParticipantAttributeTypeKeyGetAll(Guid userUniqueId, Guid tenantUniqueId, Guid correlationUniqueId, ParticipantAttributeTypeKeyIM model)
        {
            // Init query processor with query + parameters
            var query = (
                new ParticipantAttributeTypeKeyGetAllQRY(
                    tenantUniqueId: tenantUniqueId,
                    correlationUniqueId: correlationUniqueId,
                    userUniqueId: userUniqueId,
                    model
                ));

            // Validate the query
            var result = await _participantAttributeTypeKeyGetAllProcessor.ValidateAsync(query);

            // If the validation of the query was succesfull
            if (result.IsValid)
            {
                // Process the query from the persistence
                result.Object = await _participantAttributeTypeKeyGetAllProcessor.ProcessAsync(query);
            }
            return result;
        }
    }
}
		