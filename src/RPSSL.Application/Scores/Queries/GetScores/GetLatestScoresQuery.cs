using MediatR;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Scores.Queries.GetScores
{
    public sealed record GetLatestScoresQuery : IRequest<Result<IEnumerable<ScoreResponse>>>;
}
