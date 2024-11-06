using RPSSL.Domain.Implementations;
using RPSSL.Domain.Enums;

namespace RPSSL.Domain.UnitTests
{
    public class PlayServiceTests
    {
        [Fact]
        public void GetRoundResult_Should_CalculateCorrectRoundResult()
        {
            //Arrange

            var playerChoiceId = RPSSLOptions.rock;

            var oponentChoiceId = RPSSLOptions.spock;

            var playService = new PlayService();

            // Act

            var result = playService.GetRoundResult(playerChoiceId, oponentChoiceId);

            //Assert

            Assert.Equal(RPSSLResult.lose, result);
        }
    }
}