using Moq;
using RPSSL.Application.Scores.Commands.ResetScoreboard;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Const;
using RPSSL.Domain.Entities;
using RPSSL.Domain.Enums;
using System.Linq.Expressions;

namespace RPSSL.Application.UnitTests.Scores.Commands.ResetScoreboard
{
    public class ResetScoreboardCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_CallGetScoresByConditionAsyncOnce()
        {
            //Arrange

            var scoreRepositoryMock = new Mock<IScoreRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var command = new ResetScoreboardCommand();

            var handler = new ResetScoreboardCommandHandler(scoreRepositoryMock.Object, unitOfWorkMock.Object);

            //Act

            await handler.Handle(command, default);

            //Assert

            scoreRepositoryMock.Verify(
                x => x.GetScoresByConditionAsync(It.IsAny<Expression<Func<Score, bool>>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Handle_Should_CallRemoveRangeOnce()
        {
            //Arrange

            var scoreRepositoryMock = new Mock<IScoreRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var command = new ResetScoreboardCommand();

            var scoresToRemove = new List<Score> { 
                Score.Create(Guid.NewGuid(), "player 123", RPSSLOptions.paper, DefaultPlayer.DefaultPlayerOne, RPSSLOptions.rock, RPSSLResult.win, DateTime.UtcNow) 
            };

            scoreRepositoryMock.Setup(
                x => x.GetScoresByConditionAsync(It.IsAny<Expression<Func<Score, bool>>>(), It.IsAny<CancellationToken>()))
                                         .ReturnsAsync(scoresToRemove);

            var handler = new ResetScoreboardCommandHandler(scoreRepositoryMock.Object, unitOfWorkMock.Object);

            //Act

            await handler.Handle(command, default);

            //Assert
            
            scoreRepositoryMock.Verify(
                x => x.RemoveRange(It.Is<IEnumerable<Score>>(list => list == scoresToRemove)),
                                                 Times.Once);
        }

        [Fact]
        public async Task Handle_Should_CallUnitOfWorkOnce()
        {
            //Arrange

            var scoreRepositoryMock = new Mock<IScoreRepository>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var command = new ResetScoreboardCommand();

            var handler = new ResetScoreboardCommandHandler(scoreRepositoryMock.Object, unitOfWorkMock.Object);

            //Act
            await handler.Handle(command, default);

            //Assert
            unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
