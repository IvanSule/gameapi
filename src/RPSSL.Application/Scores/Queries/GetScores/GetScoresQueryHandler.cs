using MediatR;
using RPSSL.Application.Abstractions;
using RPSSL.Domain.Const;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Scores.Queries.GetScores
{
    public class GetScoresQueryHandler : IRequestHandler<GetLatestScoresQuery, Result<IEnumerable<ScoreResponse>>>
    {
        private readonly IScoresReadService _scoresReadService;

        public GetScoresQueryHandler(IScoresReadService scoresReadService)
        {
            _scoresReadService = scoresReadService;
        }
        public async Task<Result<IEnumerable<ScoreResponse>>> Handle(GetLatestScoresQuery request, CancellationToken cancellationToken)
        {
            var scores = await _scoresReadService.GetTenLatestScores(DefaultPlayer.DefaultPlayerOne);
            return scores.ToList();
        }
    }
}
