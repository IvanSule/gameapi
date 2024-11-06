using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RPSSL.Application.Play.Commands.Play;
using RPSSL.Application.Play;
using RPSSL.Presentation.Controllers;
using FluentAssertions;
using RPSSL.Domain.Enums;

namespace RPSSL.Presentation.UnitTests.Controllers
{
    public class PlayControllerTests
    {
        [Fact]
        public async Task PlayWithComputer_ShouldReturnOkWithValidPlayResponse()
        {
            //Arrange

            var playRequest = new PlayRequest(2);

            var playResponse = new PlayResponse(RPSSLResult.tie.ToString(), RPSSLOptions.paper, RPSSLOptions.paper, Guid.NewGuid());

            var senderMock = new Mock<ISender>();

            senderMock.Setup(s => s.Send(It.IsAny<PlayCommand>(), default))
                .ReturnsAsync(playResponse);

            var controller = new PlayController(senderMock.Object);

            // Act

            var result = await controller.PlayWithComputer(playRequest,default);

            // Assert

            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Match<PlayResponse>(resp => resp.Player.Equals((RPSSLOptions)playRequest.Player));
        }
    }
}
