using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Entities;
using System.Linq.Expressions;

namespace RPSSL.Infrastructure.Repositories
{
    public sealed class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ScoreRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task AddAsync(Score score)
        {
            await _dbContext.Set<Score>().AddAsync(score);
        }

        public void RemoveRange(IEnumerable<Score> scores)
        {
            _dbContext.Set<Score>().RemoveRange(scores);
        }

        public async Task<IEnumerable<Score>> GetScoresByConditionAsync(Expression<Func<Score, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Score>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }
    }
}
