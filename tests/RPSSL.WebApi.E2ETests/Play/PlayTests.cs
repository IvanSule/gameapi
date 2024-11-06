using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Application.Play;
using RPSSL.Domain.Const.ErrorCodes;
using RPSSL.WebApi.E2ETests.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace RPSSL.WebApi.E2ETests.Play
{
    public class PlayTests : BaseE2ETest
    {
        public PlayTests(E2ETestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenPlayerChoiceInvalid()
        {
            //Arrange

            var request = new PlayRequest(8);

            //Act

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("play", request);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            ValidationProblemDetails? problemDetails = await response.GetValidationProblemDetails();

            problemDetails!.Errors.SelectMany(err => err.Value).Should().Contain([PlayErrorCodes.PlayerChoiceIdInvalid]);
        }

        [Fact]
        public async Task Should_ReturnOk_WhenPlayerChoiceValid()
        {
            //Arrange

            var request = new PlayRequest(4);

            //Act

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("play", request);

            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var playResponse = await response.GetOkResult<PlayResponse>();

            playResponse.Should().NotBeNull();  
        }
    }
}