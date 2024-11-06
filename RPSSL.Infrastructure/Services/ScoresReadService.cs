using Microsoft.EntityFrameworkCore;
using RPSSL.Application.Abstractions;
using RPSSL.Application.Scores;
using RPSSL.Domain.Const;
using RPSSL.Domain.Entities;

namespace RPSSL.Infrastructure.Services
{
    internal sealed class ScoresReadService : IScoresReadService
    {
        private readonly ApplicationDbContext _context;

        public ScoresReadService(ApplicationDbContext context) {  _context = context; }

        public async Task<IEnumerable<ScoreResponse>> GetTenLatestScores(string player)
        {
            var scores = await _context.Set<Score>()
                .Where(score=>score.PlayerOne == player || score.PlayerTwo == player)
                .OrderByDescending(score => score.PlayDate)
                .Take(10)                
                .ToListAsync();

            return scores.Select(score => new ScoreResponse
                (
                    score.PlayerOne,
                    score.PlayerTwo,
                    score.Result.ToString(),
                    score.PlayDate
                ));
        }
    }
}
