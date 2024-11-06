using RPSSL.Application.Scores;

namespace RPSSL.Application.Abstractions
{
    public interface IScoresReadService
    {
        Task<IEnumerable<ScoreResponse>> GetTenLatestScores(string player);
    }
}
