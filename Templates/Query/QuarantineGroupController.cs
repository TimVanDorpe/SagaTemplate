 using HC.Common;
using HC.Common.Presentation.Logging;
using HC.Common.Presentation.Security;
using HC.Core.Application;
using HC.Core.Application.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Core.Api.Areas.API.Controllers
{
    [AuthorizeTenant(NeedsAdminAccess = false)]
    [Route("V4.0/{cultureInfo:length(2):alpha}/tenant/{tenantUniqueId:guid}/QuarantineGroup")]
    public class QuarantineGroupController : BaseController
    {
        private readonly QuarantineGroupAS _quarantineGroupAS;
        private readonly AppSettings _appSettings;

        public QuarantineGroupController(QuarantineGroupAS quarantineGroupAS, GeneralExceptionHandling exception, ICrudLogger logger, ApiMembership membership , AppSettings appSettings)
            : base(exception, logger, membership)
        {
           Condition.Requires(_quarantineGroupAS, nameof(quarantineGroupAS)).IsNotNull();
           appSettings.Requires().IsNotNull();

            _quarantineGroupAS = quarantineGroupAS;
            _appSettings = appSettings;         
        }

		[ProducesResponseType(statusCode: 200, type: typeof(QuarantineGroupVM))]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpGet("QuarantineGroupGetById/{uniqueId:Guid}")]
        public async Task<IActionResult> QuarantineGroupGetById(
            [FromRoute] string cultureInfo,
            [FromRoute] Guid tenantUniqueId,
            [FromRoute] QuarantineGroupIM model           
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
                var result = await quarantineGroupAS.QuarantineGroupGetById(this.Membership.GetToken().User.Id,
                    tenantUniqueId,
                    queryUniqueId.Value,
                    model
                    );

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
