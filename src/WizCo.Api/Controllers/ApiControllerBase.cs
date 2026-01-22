using Microsoft.AspNetCore.Mvc;
using System.Net;
using WizCo.Application.Shared.Results;
using WizCo.Application.Interfaces.Services;

namespace WizCo.Api.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IServiceContext _serviceContext;

        protected ApiControllerBase(IServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        protected IActionResult ServiceResponse(HttpStatusCode statusCode)
        {
            return ServiceResponse<object>(null, statusCode);
        }

        protected IActionResult ServiceResponse<T>(T result, HttpStatusCode statusCode)
        {
            if (_serviceContext.HasNotification())
            {
                return BadRequest(new ApiBadRequestResult(_serviceContext.Notifications));
            }

            return statusCode switch
            {
                HttpStatusCode.Created => new ObjectResult(result) { StatusCode = StatusCodes.Status201Created },
                HttpStatusCode.NoContent => NoContent(),
                _ => result is null ? Ok() : Ok(result),
            };
        }
    }
}
