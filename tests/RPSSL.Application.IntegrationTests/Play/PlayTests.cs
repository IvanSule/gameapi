using RPSSL.Application.Exceptions;
using RPSSL.Application.Play.Commands.Play;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RPSSL.Domain.Entities;

namespace RPSSL.Application.IntegrationTests.Play
{
    public class PlayTests : BaseIntegrationTest
    {
        public PlayTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Play_Should_SaveNewScoreToDatabase_WhenPlayCommandIsValid()
        {
            //Arrange

            var command = new PlayCommand(5);

            //Act
            var response = await Sender.Send(command, default);

            //Assert

            var score = await DbContext.Set<Score>().FirstOrDefaultAsync(score => score.Id == response.Value.ScoreId); 

            score.Should().NotBeNull();
        }

        [Fact]
        public async Task Play_Should_ThrowValidationException_WhenPlayCommandIsInvalid()
        {
            //Arrange

            var command = new PlayCommand(7);

            //Act

            Func<Task> action = () => Sender.Send(command, default);

            //Assert

            await action.Should().ThrowAsync<CustomValidationException>();
        }
    }
}