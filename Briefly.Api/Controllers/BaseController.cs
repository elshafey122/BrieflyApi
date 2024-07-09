using Briefly.Core.Response;
using Briefly.Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Briefly.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ObjectResult Result<T>(Response<T> response)
        {
           switch (response.StatusCode)
            {
                case(HttpStatusCode.OK):
                    return new OkObjectResult(response);
                    break;
                case (HttpStatusCode.Unauthorized):
                    return new UnauthorizedObjectResult(response);
                    break;
                case (HttpStatusCode.NotFound):
                    return new NotFoundObjectResult(response);
                    break;
                case (HttpStatusCode.BadRequest):
                    return new BadRequestObjectResult(response);
                    break;
                case (HttpStatusCode.Created):
                    return new CreatedResult(string.Empty,response);
                    break;
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }


        }

        protected string GetUserId()
        {
            var userid = HttpContext?.User.FindFirstValue((nameof(UserClaimTypes.UserId)));
            return userid;
        }
    }
}
