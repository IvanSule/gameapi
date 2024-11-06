using Moq;
using RPSSL.Application.Play.Commands.Play;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Entities;
using RPSSL.Domain.Enums;
using RPSSL.Application.Abstractions;

namespace RPSS.Application.UnitTests.Play.Commands.Play
{
    public class PlayCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_CallAddOnRepositoryOnce_WhenPlayerIsValid()
        {
            //Arrange

            var choiceServiceMock = new Mock<IRandomChoiceService>();

            var playServiceMock = new Mock<IPlayService>();

            var scoreRepositoryMock = new Mock<IScoreRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var command = new PlayCommand(5);

            choiceServiceMock.Setup(x => x.GetRandomChoiceAsync()).ReturnsAsync(RPSSLOptions.rock);

            playServiceMock.Setup(x => x.GetRoundResult(It.IsAny<RPSSLOptions>(), It.IsAny<RPSSLOptions>())).Returns(RPSSLResult.win);

            var handler = new PlayCommandHandler(choiceServiceMock.Object, scoreRepositoryMock.Object, unitOfWorkMock.Object, playServiceMock.Object);

            //Act

            var result = await handler.Handle(command, default);

            //Assert

            scoreRepositoryMock.Verify(
                x => x.AddAsync(It.Is<Score>(score => score.PlayerOneChoice == result.Value.Player
                                                 && score.PlayerTwoChoice == result.Value.Computer
                                                 && score.Result.ToString() == result.Value.Results)),
                                                 Times.Once);
        }

        [Fact]
        public async Task Handle_Should_CallUnitOfWorkOnce_WhenPlayerIsValid()
        {
            //Arrange

            var choiceServiceMock = new Mock<IRandomChoiceService>();

            var playServiceMock = new Mock<IPlayService>();

            var scoreRepositoryMock = new Mock<IScoreRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var command = new PlayCommand(5);

            choiceServiceMock.Setup(x => x.GetRandomChoiceAsync()).ReturnsAsync(RPSSLOptions.rock);

            playServiceMock.Setup(x => x.GetRoundResult(It.IsAny<RPSSLOptions>(), It.IsAny<RPSSLOptions>())).Returns(RPSSLResult.win);

            var handler = new PlayCommandHandler(choiceServiceMock.Object, scoreRepositoryMock.Object, unitOfWorkMock.Object, playServiceMock.Object);

            //Act

            await handler.Handle(command, default);

            //Assert

            unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}