using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Application.Play;
using RPSSL.Application.Play.Commands.Play;
using RPSSL.Presentation.Abstractions;

namespace RPSSL.Presentation.Controllers
{
    public class PlayController : ApiController
    {
        public PlayController(ISender sender)
        : base(sender)
        {
        }

        /// <summary>
        /// Play a round against computer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Play result with both choices</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayResponse), StatusCodes.Status200OK)]
        [Route("play")]
        public async Task<IActionResult> PlayWithComputer([FromBody] PlayRequest request, CancellationToken cancellationToken)
        {
            var command = new PlayCommand(request.Player);

            var response = await Sender.Send(command, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : CreateProblemDetails400Response(response);
        }
    }
}
