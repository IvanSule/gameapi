using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Application.Choices;
using RPSSL.Application.Choices.Queries.GetChoices;
using RPSSL.Application.Choices.Queries.GetRandomChoice;
using RPSSL.Presentation.Abstractions;

namespace RPSSL.WebApi.Controllers
{    
    public class ChoicesController : ApiController
    {
        public ChoicesController(ISender sender)
        : base(sender)
        {
        }

        /// <summary>
        /// Get all choices visible to UI
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>List of choices</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ChoiceResponse>), StatusCodes.Status200OK)]
        [Route("choices")]
        public async Task<IActionResult> GetAllChoices(CancellationToken cancellationToken)
        {
            var query = new GetChoicesQuery();

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : CreateProblemDetails400Response(response);
        }

        /// <summary>
        /// Get a randonly generated choice
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Choice</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ChoiceResponse), StatusCodes.Status200OK)]
        [Route("choice")]
        public async Task<IActionResult> GetRandomChoice(CancellationToken cancellationToken)
        {
            var query = new GetRandomChoiceQuery();

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : CreateProblemDetails400Response(response);
        }
    }
}
