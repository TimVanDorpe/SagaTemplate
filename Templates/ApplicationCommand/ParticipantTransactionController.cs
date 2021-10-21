using HC.Common.Presentation.Logging;
using Microsoft.AspNetCore.Mvc;
using HC.Common.Presentation.Security;
using HC.Common;
using System.Threading.Tasks;
using System;
using HC.Core.Application;
using HC.Core.Application.Models.InputModel;

namespace HC.Core.Api.Areas.API.Controllers
{
    [Route("V4.0/{cultureInfo:length(2):alpha}/tenant")]
    public class ParticipantTransactionController : BaseController
    {
        private readonly ParticipantTransactionAS _participantTransactionAS;

        public ParticipantTransactionController(
            ParticipantTransactionAS participantTransactionAS,
            GeneralExceptionHandling exception,
            ICrudLogger logger,
            ApiMembership membership)
            : base(exception, logger, membership)
        {
            _participantTransactionAS = participantTransactionAS;
        }

        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpPost("AddParticipantTransactionBatch")]
        public async Task<IActionResult> AddParticipantTransactionBatch(
            [FromRoute] string cultureInfo,
            [FromBody] AddParticipantTransactionBatchIM model
            )
        {
            // Generate CorrelationUniqueId
            model.CorrelationUniqueId = Guid.NewGuid();

            // Handle possible exceptions
            return await this.Exception.HandleForLoggingAsync(model.CorrelationUniqueId, async () =>
            {
                 // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(this.Membership.GetToken().User.Id), UniqueId.CreateIfNotNull(model.CorrelationUniqueId), model);

                // Handle request
                var result = await _participantTransactionAS.AddParticipantTransactionBatch(this.Membership.GetToken().User.Id, model);

                // Return response
                if (result.IsValid)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            });
        }
    }
}
 