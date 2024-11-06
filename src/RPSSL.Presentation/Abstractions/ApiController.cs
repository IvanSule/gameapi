using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Domain.Models;

namespace RPSSL.Presentation.Abstractions
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        protected ApiController(ISender sender) => Sender = sender;

        protected IActionResult CreateProblemDetails400Response(Result result)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Bad Request",
                Type = result.Error.Code,
                Detail = result.Error.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
