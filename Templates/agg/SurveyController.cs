using HC.Common;
using HC.Common.Presentation.Security;
using HC.LegacySync.Api.Areas.API.Controllers;
using HC.LegacySync.Application;
using HC.LegacySync.Application.Models.InputModel.Survey;
using HC.LegacySync.Application.Models.ViewModel.Survey;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HC.LegacySync.Api.Areas.API.Controllers
{
    [AuthorizeTenant(NeedsAdminAccess = true)]
    [Route("V4.0/{cultureInfo:length(2):alpha}/tenant/{tenantUniqueId:guid}/Survey")]
    public class SurveyController : BaseController
    {
        private readonly SurveyAS surveyAS;
        private readonly AppSettings appSettings;

        public SurveyController()
        {
            surveyAS = ObjectContainer.Resolve<SurveyAS>();
            appSettings = ObjectContainer.Resolve<AppSettings>();

            Condition.Requires(surveyAS, nameof(surveyAS)).IsNotNull();
            Condition.Requires(appSettings, nameof(appSettings)).IsNotNull();
        }

        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(
            [FromRoute]string cultureInfo,
            [FromBody]CreateASurveyIM model)
        {
            // Handle possible exceptions
            return await Exception.HandleForLoggingAsync(model.CorrelationUniqueId, model, async () =>
            {
                // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(Membership.GetToken().User.Id), UniqueId.CreateIfNotNull(model.CorrelationUniqueId), model);

                // Handle request
                var result = await surveyAS.CreateAsync(Membership.GetToken().User.Id, model);

                // Log response
                Log.Response(Request, sw, UniqueId.CreateIfNotNull(model.CorrelationUniqueId), result);

                // Return response
                if (result.IsValid)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            });
        }

        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(
            [FromRoute]string cultureInfo,
            [FromBody]UpdateSurveyIM model)
        {
            // Handle possible exceptions
            return await Exception.HandleForLoggingAsync(model.CorrelationUniqueId, model, async () =>
            {
                // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(Membership.GetToken().User.Id), UniqueId.CreateIfNotNull(model.CorrelationUniqueId), model);

                // Handle request
                var result = await surveyAS.UpdateAsync(Membership.GetToken().User.Id, model);

                // Log response
                Log.Response(Request, sw, UniqueId.CreateIfNotNull(model.CorrelationUniqueId), result);

                // Return response
                if (result.IsValid)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            });
        }


        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpPut("Delete")]
        public async Task<IActionResult> Delete(
            [FromRoute]string cultureInfo,
            [FromBody]DeleteASurveyIM model)
        {
            // Handle possible exceptions
            return await Exception.HandleForLoggingAsync(model.CorrelationUniqueId, model, async () =>
            {
                // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(Membership.GetToken().User.Id), UniqueId.CreateIfNotNull(model.CorrelationUniqueId), model);

                // Handle request
                var result = await surveyAS.DeleteAsync(Membership.GetToken().User.Id, model);

                // Log response
                Log.Response(Request, sw, UniqueId.CreateIfNotNull(model.CorrelationUniqueId), result);

                // Return response
                if (result.IsValid)
				    return Ok();
                else
                    return BadRequest(result.Errors);
 
            });
        }


        [ProducesResponseType(statusCode: 200, type: typeof(SurveyVM[]))]
        [ProducesResponseType(statusCode: 400, type: typeof(ValidationError[]))]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(
            [FromRoute] string cultureInfo,
            [FromRoute] Guid tenantUniqueId
            )
        {
            // Generate query id
            var queryUniqueId = UniqueId.Generate();

            // Handle possible exceptions
            return await Exception.HandleForLoggingAsync(queryUniqueId.Value, async () =>
            {
                // Log request
                var sw = Log.Request(Request, UniqueId.CreateIfNotNull(Membership.GetToken().User.Id), queryUniqueId);

                // Handle request
                var result = await surveyAS.GetAllAsync(appSettings.Security.SuperTenantUniqueId, queryUniqueId.Value, Membership.GetToken().User.Id);

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
