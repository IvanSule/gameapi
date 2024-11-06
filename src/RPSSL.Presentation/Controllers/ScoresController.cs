using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Application.Scores;
using RPSSL.Application.Scores.Commands.ResetScoreboard;
using RPSSL.Application.Scores.Queries.GetScores;
using RPSSL.Presentation.Abstractions;

namespace RPSSL.Presentation.Controllers
{
    public class ScoresController : ApiController
    {
        public ScoresController(ISender sender)
        : base(sender)
        {
        }

        /// <summary>
        /// Get 10 most recent scores
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>List of scores</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ScoreResponse>), StatusCodes.Status200OK)]
        [Route("scores/getLatestScores")]
        public async Task<IActionResult> GetLatestScores(CancellationToken cancellationToken)
        {
            var query = new GetLatestScoresQuery();

            var response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : CreateProblemDetails400Response(response); ;
        }

        /// <summary>
        /// Reset scoreboard
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>List of scores</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("scores/resetScoreboard")]
        public async Task<IActionResult> ResetScoreboard(CancellationToken cancellationToken)
        {
            var query = new ResetScoreboardCommand();

            await Sender.Send(query, cancellationToken);

            return NoContent();
        }
    }
}
