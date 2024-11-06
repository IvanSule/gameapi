using FluentAssertions;
using Moq;
using RPSSL.Application.Abstractions;
using RPSSL.Domain.Enums;
using RPSSL.Domain.Exceptions;
using RPSSL.Domain.Implementations;
using RPSSL.Domain.Models;

namespace RPSSL.Infrastructure.UnitTests.Services
{
    public class RandomChoiceServiceTests
    {
        [Fact]
        public async Task GetRoundResult_Should_ThrowRandomNumberServiceException_WhenRandomNumberHttpServiceReturnsNull()
        {
            //Arrange

            var randomNumberHttpServiceMock = new Mock<IRandomNumberHttpService>();

            randomNumberHttpServiceMock.Setup(x => x.GetRandomNumberResponseAsync()).ReturnsAsync((BoohmaRandomNumberResponse?)null);

            var choiceService = new RandomChoiceService(randomNumberHttpServiceMock.Object);

            // Act

            Func<Task> action = async () => await choiceService.GetRandomChoiceAsync();

            //Assert

            await action.Should().ThrowAsync<RandomNumberServiceException>();
        }

        [Fact]
        public async Task GetRoundResult_Should_ReturnChoice_WhenRandomNumberHttpServiceReturnsValidObject()
        {
            //Arrange

            var randomNumberHttpServiceMock = new Mock<IRandomNumberHttpService>();

            var randomNumberResponse = new BoohmaRandomNumberResponse(23);

            randomNumberHttpServiceMock.Setup(x => x.GetRandomNumberResponseAsync()).ReturnsAsync(randomNumberResponse);

            var choiceService = new RandomChoiceService(randomNumberHttpServiceMock.Object);

            // Act

            var result = await choiceService.GetRandomChoiceAsync();

            //Assert

            Assert.Equal(RPSSLOptions.paper, result);
        }
    }
}
