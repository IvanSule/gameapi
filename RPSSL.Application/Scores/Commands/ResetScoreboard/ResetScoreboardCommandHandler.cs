using MediatR;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Const;

namespace RPSSL.Application.Scores.Commands.ResetScoreboard
{
    internal class ResetScoreboardCommandHandler : IRequestHandler<ResetScoreboardCommand>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResetScoreboardCommandHandler(IScoreRepository scoreRepository, IUnitOfWork unitOfWork)
        {
            _scoreRepository = scoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ResetScoreboardCommand request, CancellationToken cancellationToken)
        {
            var scores = await _scoreRepository.GetScoresByConditionAsync(score=> score.PlayerOne == DefaultPlayer.DefaultPlayerOne || score.PlayerTwo == DefaultPlayer.DefaultPlayerOne, cancellationToken);

            _scoreRepository.RemoveRange(scores);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
