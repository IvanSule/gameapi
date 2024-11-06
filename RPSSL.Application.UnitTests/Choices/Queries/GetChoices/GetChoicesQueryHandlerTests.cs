using FluentAssertions;
using Moq;
using RPSSL.Application.Choices;
using RPSSL.Application.Choices.Queries.GetChoices;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Enums;

namespace RPSSL.Application.UnitTests.Choices.Queries.GetChoices
{
    public class GetChoicesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_ReturnListofChoices()
        {
            //Arrange

            var playServiceMock = new Mock<IPlayService>();

            var serviceChoices = new List<RPSSLOptions>
            {
                RPSSLOptions.rock,
                RPSSLOptions.paper,
                RPSSLOptions.scissors,
                RPSSLOptions.lizard,
                RPSSLOptions.spock
            };

            var expectedChoices = serviceChoices.Select(item => new ChoiceResponse(item, item.ToString())).ToList();

            playServiceMock.Setup(x => x.GetAllChoices()).Returns(serviceChoices);

            var handler = new GetChoicesQueryHandler(playServiceMock.Object);

            var query = new GetChoicesQuery();

            //Act

            var result = await handler.Handle(query, default);

            //Assert

            result.Value.Should().NotBeNull();

            result.Value.Should().BeEquivalentTo(expectedChoices);
        }
    }
}
