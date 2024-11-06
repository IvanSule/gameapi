using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RPSSL.Application.Choices;
using RPSSL.Application.Choices.Queries.GetChoices;
using RPSSL.WebApi.Controllers;
using FluentAssertions;
using RPSSL.Domain.Enums;
using RPSSL.Domain.Models;

namespace RPSSL.Presentation.UnitTests.Controllers
{
    public class ChoicesControllerTests
    {
        [Fact]
        public async Task GetAllChoices_ShouldReturnOkWithListOfChoices()
        {
            //Arrange

            var senderMock = new Mock<ISender>();

            var serviceChoices = new List<RPSSLOptions>
            {
                RPSSLOptions.rock,
                RPSSLOptions.paper,
                RPSSLOptions.scissors,
                RPSSLOptions.lizard,
                RPSSLOptions.spock
            };

            var expectedChoices = serviceChoices.Select(item => new ChoiceResponse(item, item.ToString()));

            senderMock.Setup(s => s.Send(It.IsAny<GetChoicesQuery>(), default))
                .ReturnsAsync(Result.Success(expectedChoices));

            var controller = new ChoicesController(senderMock.Object);

            // Act

            var result = await controller.GetAllChoices(default);

            // Assert

            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeEquivalentTo(expectedChoices);
        }
    }
}
