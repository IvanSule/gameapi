using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Application.Scores.Commands.ResetScoreboard;
using RPSSL.Domain.Const;
using RPSSL.Domain.Entities;
using RPSSL.Domain.Enums;

namespace RPSSL.Application.IntegrationTests.Scores
{
    public class ResetScoreboardTests : BaseIntegrationTest
    {
        public ResetScoreboardTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ResetScoreboard_Should_RemoveAllScoresOfDefaultPlayerOne()
        {
            //Arrange

            var command = new ResetScoreboardCommand();

            Guid newScore = Guid.NewGuid();

            await DbContext.Set<Score>().AddAsync(Score.Create(newScore, DefaultPlayer.DefaultPlayerOne, RPSSLOptions.paper, DefaultPlayer.Computer, RPSSLOptions.rock, RPSSLResult.win, DateTime.UtcNow));

            await DbContext.SaveChangesAsync();

            //Act

            await Sender.Send(command, default);

            //Assert

            var score = await DbContext.Set<Score>().FirstOrDefaultAsync(score => score.Id == newScore);

            score.Should().BeNull();
        }
    }
}
