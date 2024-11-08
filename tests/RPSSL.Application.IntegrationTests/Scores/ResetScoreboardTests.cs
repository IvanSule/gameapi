﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Application.Scores.Commands.ResetScoreboard;
using RPSSL.Domain.Const;
using RPSSL.Domain.Entities;
using RPSSL.Domain.Enums;
using RPSSL.Domain.Primitives;

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

            var mockScore = Score.Create(newScore, DefaultPlayer.DefaultPlayerOne, RPSSLOptions.paper, DefaultPlayer.Computer, RPSSLOptions.rock, RPSSLResult.win, DateTime.UtcNow);

            await DbContext.Set<Score>().AddAsync(mockScore);

            await DbContext.SaveChangesAsync();

            DbContext.Entry(mockScore).State = EntityState.Detached;

            //Act

            await Sender.Send(command, default);

            //Assert

            var score = await DbContext.Set<Score>().FirstOrDefaultAsync(score => score.Id == newScore);

            score.Should().BeNull();
        }
    }
}
