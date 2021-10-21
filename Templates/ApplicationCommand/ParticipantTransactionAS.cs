using HC.Common;
using HC.Core.Application.Command;
using HC.Core.Application.Models.InputModel;
using HC.Core.Application.Models.ViewModel;
using HC.Core.Application.Query;
using HC.Integration.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace HC.Core.Application
{
    public class ParticipantTransactionAS
    {
        private readonly ICommandProcessor<AddParticipantTransactionBatchCMD> _addParticipantTransactionBatchProcessor;       

        public ParticipantTransactionAS(
            ICommandProcessor<AddParticipantTransactionBatchCMD> addParticipantTransactionBatchProcessor,
          )
        {
            _addParticipantTransactionBatchProcessor = addParticipantTransactionBatchProcessor;           
        }

        public async Task<Result> AddParticipantTransactionBatch(Guid userUniqueId , AddParticipantTransactionBatchIM model)
        {
            // Init command processor with query + parameters
            var command =
                new AddParticipantTransactionBatchCMD(
                    model.TenantUniqueId,
                    model.CorrelationUniqueId,
                    userUniqueId,
                                
            model.addParticipantTransactionBatch);
            // Validate the command
            var result = await _addParticipantTransactionBatchProcessor.ValidateAsync(command);

            // If the validation of the command was succesfull
            if (result.IsValid)
                // Process the command from the persistence
                await _addParticipantTransactionBatchProcessor.ProcessAsync(command);
            return result;
        }      
    }
}
 