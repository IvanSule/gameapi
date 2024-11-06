using MediatR;
using RPSSL.Application.Abstractions;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Const;
using RPSSL.Domain.Entities;
using RPSSL.Domain.Enums;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Play.Commands.Play
{
    internal class PlayCommandHandler : IRequestHandler<PlayCommand, Result<PlayResponse>>
    {
        private readonly IRandomChoiceService _randomChoiceService;
        private readonly IPlayService _playService;
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlayCommandHandler(IRandomChoiceService randomChoiceService, IScoreRepository scoreRepository, IUnitOfWork unitOfWork, IPlayService playService)
        {
            _randomChoiceService = randomChoiceService;
            _playService = playService;
            _scoreRepository = scoreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PlayResponse>> Handle(PlayCommand request, CancellationToken cancellationToken)
        {
            var randomChoice = await _randomChoiceService.GetRandomChoiceAsync();

            var playerChoiceId = (RPSSLOptions)request.Player;

            var playResult = _playService.GetRoundResult(playerChoiceId, randomChoice);

            var scoreId = Guid.NewGuid();

            var score = Score.Create(scoreId, DefaultPlayer.DefaultPlayerOne, playerChoiceId, DefaultPlayer.Computer, randomChoice, playResult, DateTime.UtcNow);

            await _scoreRepository.AddAsync(score);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PlayResponse(playResult.ToString(), playerChoiceId, randomChoice, score.Id);
        }
    }
}
