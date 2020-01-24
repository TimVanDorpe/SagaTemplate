
using HC.Isaac.Application.Query;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Isaac.Application.Models.ViewModel.PosWordString;
using HC.Isaac.Application.Models.InputModel.PosWordString;
using HC.Isaac.Application.Command;

namespace HC.Isaac.Application
{
    public class PosWordStringAS
    {

        public async Task<Result<IEnumerable<PosWordStringVM>>> GetAllAsync(Guid tenantUniqueId, Guid correlationUniqueId, Guid userUniqueId)
        {
            // Init query processor with query + parameters
            var queryProcessor = new QueryProcessor<PosWordStringGetAllQRY, IEnumerable<PosWordStringVM>>(new PosWordStringGetAllQRY(tenantUniqueId, correlationUniqueId, userUniqueId));

            // Validate the query
            var result = await queryProcessor.ValidateAsync();

            // If the validation of the query was succesfull
            if (result.IsValid)
            {
                // Process the query from the persistence
                result.Object = await queryProcessor.ProcessAsync();
            }

            return result;
        }
        public async Task<Result> CreateAsync(Guid userUniqueId, CreateAPosWordStringIM model)
        {
            // Init command processor with parameters
            var commandProcessor = new CommandProcessor<CreateAPosWordStringCMD>(
                new CreateAPosWordStringCMD(
                    // Default props
                    tenantUniqueId: model.TenantUniqueId,
                    correlationUniqueId: model.CorrelationUniqueId,
                    userUniqueId: userUniqueId
					, model.Text
					, model.Language
					, model.DefaultPosPolarity
   
                    ));

            // Validate the query
            var result = await commandProcessor.ValidateAsync();

            // If the validation of the query was succesfull
            if (result.IsValid)
            {
                // Process the query from the persistence
                await commandProcessor.ProcessAsync();
            }
            return result;
        }
        public async Task<Result> DeleteAsync(Guid userUniqueId, DeleteAPosWordStringIM model)
        {
            // Init command processor with parameters
            var commandProcessor = new CommandProcessor<DeleteAPosWordStringCMD>(
                new DeleteAPosWordStringCMD(
                    // Default props
                    tenantUniqueId: model.TenantUniqueId,
                    correlationUniqueId: model.CorrelationUniqueId,
                    userUniqueId: userUniqueId,
                    version: model.Version,                    
                    uniqueId: model.UniqueId == null ? Guid.Empty : model.UniqueId
                ));

            // Validate the query
            var result = await commandProcessor.ValidateAsync();

            // If the validation of the query was succesfull
            if (result.IsValid)
            {
                // Process the query from the persistence
                await commandProcessor.ProcessAsync();
            }
            return result;
        }  
    }
}
