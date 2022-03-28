
using HC.LegacySync.Application.Query;
using HC.Common;
using HC.Integration.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.LegacySync.Application.Models.ViewModel.Survey;
using HC.LegacySync.Application.Models.InputModel.Survey;
using HC.LegacySync.Application.Command;

namespace HC.LegacySync.Application
{
    public class SurveyAS
    {

        public async Task<Result<IEnumerable<SurveyVM>>> GetAllAsync(Guid tenantUniqueId, Guid correlationUniqueId, Guid userUniqueId)
        {
            // Init query processor with query + parameters
            var queryProcessor = new QueryProcessor<SurveyGetAllQRY, IEnumerable<SurveyVM>>(new SurveyGetAllQRY(tenantUniqueId, correlationUniqueId, userUniqueId));

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
        public async Task<Result> CreateAsync(Guid userUniqueId, CreateASurveyIM model)
        {
            // Init command processor with parameters
            var commandProcessor = new CommandProcessor<CreateASurveyCMD>(
                new CreateASurveyCMD(
                    // Default props
                    tenantUniqueId: model.TenantUniqueId,
                    correlationUniqueId: model.CorrelationUniqueId,
                    userUniqueId: userUniqueId
					, model.TouchpointUniqueId
   
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
public async Task<Result> UpdateAsync(Guid userUniqueId, UpdateASurveyIM model)
        {
            // Init command processor with parameters
            var commandProcessor = new CommandProcessor<UpdateASurveyCMD>(
                new UpdateASurveyCMD(
                    // Default props
                    tenantUniqueId: model.TenantUniqueId,
                    correlationUniqueId: model.CorrelationUniqueId,
                    userUniqueId: userUniqueId,
					version : model.Version,
					, model.TouchpointUniqueId
   
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
        public async Task<Result> DeleteAsync(Guid userUniqueId, DeleteASurveyIM model)
        {
            // Init command processor with parameters
            var commandProcessor = new CommandProcessor<DeleteASurveyCMD>(
                new DeleteASurveyCMD(
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
