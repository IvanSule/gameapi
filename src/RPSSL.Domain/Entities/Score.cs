using RPSSL.Domain.Enums;
using RPSSL.Domain.Primitives;

namespace RPSSL.Domain.Entities
{
    public sealed class Score : Entity
    {
        private Score(Guid id, string playerOne, RPSSLOptions playerOneChoice, string playerTwo, RPSSLOptions playerTwoChoice, RPSSLResult result, DateTime playDate)
        : base(id)
        {
            PlayerOne = playerOne;
            PlayerOneChoice = playerOneChoice;
            PlayerTwo = playerTwo;
            PlayerTwoChoice = playerTwoChoice;
            Result = result;
            PlayDate = playDate;
        }

        private Score()
        {
        }

        public string PlayerOne { get; private set; }
        public RPSSLOptions PlayerOneChoice { get; private set; }
        public string PlayerTwo { get; private set; }
        public RPSSLOptions PlayerTwoChoice { get; private set; }
        public RPSSLResult Result { get; private set; }
        public DateTime PlayDate { get; private set; }

        public static Score Create(Guid id, string playerOne, RPSSLOptions playerOneChoice, string playerTwo, RPSSLOptions playerTwoChoice, RPSSLResult result, DateTime playDate)
        {
            var score = new Score(id, playerOne, playerOneChoice, playerTwo, playerTwoChoice, result, playDate);

            return score;
        }
    }
}
