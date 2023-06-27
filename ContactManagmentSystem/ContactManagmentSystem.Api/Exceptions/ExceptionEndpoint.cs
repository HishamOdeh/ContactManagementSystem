using Ardalis.ApiEndpoints;
using ContactManagementSystem.Api.Extensions;
using ContactManagementSystem.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagementSystem.Api.Exceptions
{
    public class ExceptionEndpoint : BaseEndpoint
        .WithoutRequest
        .WithResponse<CommandResponse>
    {
        [Route("/exception")]
        [SwaggerOperation(
               Summary = "Exception Endpoint",
               Description = "Any unhandled exception thrown in any endpoint will be caught here, and that exception message(s) will get added as a model state error and the command response will be returned to the frontend.",
               OperationId = "Exception.ConvertToModelStateError",
               Tags = new[] { "ExceptionEndpoint" })]
        public override ActionResult<CommandResponse> Handle()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ModelState.AddModelError(string.Empty, exception.Error.Message);
#if DEBUG
            if (exception.Error.InnerException != null)
            {
                ModelState.AddModelError(string.Empty, exception.Error.InnerException.Message);
            }
#endif
            return new ObjectResult(new CommandResponse().Errors(ModelState));
        }
    }
}