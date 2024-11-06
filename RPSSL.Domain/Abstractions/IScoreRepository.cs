using RPSSL.Domain.Entities;
using System.Linq.Expressions;

namespace RPSSL.Domain.Abstractions
{
    public interface IScoreRepository
    {
        Task AddAsync(Score score);

        void RemoveRange(IEnumerable<Score> scores);

        Task<IEnumerable<Score>> GetScoresByConditionAsync(Expression<Func<Score, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
