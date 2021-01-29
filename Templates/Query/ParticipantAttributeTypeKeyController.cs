 using HC.Common;
using HC.Common.Presentation.Logging;
using HC.Common.Presentation.Security;
using HC.Core.Application;
using HC.Core.Application.Models.ViewModel.ParticipantAttributeTypeKey;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Core.Api.Areas.API.Controllers
{
    [AuthorizeTenant(NeedsAdminAccess = false)]
    [Route("V4.0/{cultureInfo:length(2):alpha}/tenant/{tenantUniqueId:guid}/ParticipantAttributeTypeKey")]
    public class ParticipantAttributeTypeKeyController : BaseController
    {
        private readonly ParticipantAttributeTypeKeyAS _participantAttributeTypeKeyAS;
         private readonly AppSettings _appSettings;

        public ParticipantAttributeTypeKeyController(ParticipantAttributeTypeKeyAS participantAttributeTypeKeyAS, GeneralExceptionHandling exception, ICrudLogger logger, ApiMembership membership , AppSettings appSettings)
            : base(exception, logger, membership)
        {
           Condition.Requires(_participantAttributeTypeKeyAS, nameof(participantAttributeTypeKeyAS)).IsNotNull();
           appSettings.Requires().IsNotNull();

            _participantAttributeTypeKeyAS = participantAttributeTypeKeyAS;
            _appSettings = appSettings;         
        }

		[ProducesResponseType(statusCode: 200, type: typeof(ParticipantAttributeTypeKeyVM))]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpGet("ParticipantAttributeTypeKeyGetAll")]
        public async Task<IActionResult> ParticipantAttributeTypeKeyGetAll(
            [FromRoute] string cultureInfo,
            [FromRoute] Guid tenantUniqueId,
            [FromRoute] ParticipantAttributeTypeKey ParticipantAttributeTypeKeyIM
        )
        {
            // Generate query id
            var queryUniqueId = UniqueId.Generate();

            // Handle possible exceptions
            return await this.Exception.HandleForLoggingAsync(queryUniqueId.Value, async () =>
            {
                // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(this.Membership.GetToken().User.Id), queryUniqueId);

                // supertenant _appSettings.Security.SuperTenantUniqueId
                // Handle request
                var result = await participantAttributeTypeKeyAS.ParticipantAttributeTypeKeyGetAll(this.Membership.GetToken().User.Id,
                    tenantUniqueId,
                    queryUniqueId.Value,
                    ParticipantAttributeTypeKeyIM);

                // Log response
                Log.Response(Request, sw, queryUniqueId, result);

                // Return response
                if (result.IsValid)
                    return Ok(result.Object);
                else
                    return BadRequest(result.Errors);
            });
        }
    }
}
