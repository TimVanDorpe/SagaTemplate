using HC.Common;
using HC.Core.Application.Models.ViewModel;
using HC.Core.Application.Models.InputModel;
using HC.Core.Application.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Core.Application
{
    public class QuarantineGroupAS
    {
    private readonly IQueryProcessor<QuarantineGroupGetByIdQRY, QuarantineGroupVM> _quarantineGroupGetByIdProcessor;

     public QuarantineGroupAS(IQueryProcessor<QuarantineGroupGetByIdQRY, QuarantineGroupVM> quarantineGroupGetByIdProcessor)
        {
            _quarantineGroupGetByIdProcessor = quarantineGroupGetByIdProcessor;
        }

public async Task<Result<QuarantineGroupVM>> QuarantineGroupGetById(Guid userUniqueId, Guid tenantUniqueId, Guid correlationUniqueId, QuarantineGroupIM model)
        {
            // Init query processor with query + parameters
            var query = (
                new QuarantineGroupGetByIdQRY(
                    tenantUniqueId: tenantUniqueId,
                    correlationUniqueId: correlationUniqueId,
                    userUniqueId: userUniqueId,
                    model: QuarantineGroupIM
                ));

            // Validate the query
            var result = await _quarantineGroupGetByIdProcessor.ValidateAsync(query);

            // If the validation of the query was succesfull
            if (result.IsValid)
            {
                // Process the query from the persistence
                result.Object = await _quarantineGroupGetByIdProcessor.ProcessAsync(query);
            }
            return result;
        }
    }
}
		