using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Enums;

namespace RPSSL.Domain.Implementations
{
    public class PlayService : IPlayService
    {
        public static List<RPSSLOptions> Choices = new List<RPSSLOptions>
        {
            RPSSLOptions.rock,
            RPSSLOptions.paper,
            RPSSLOptions.scissors,
            RPSSLOptions.lizard,
            RPSSLOptions.spock
        };

        public IEnumerable<RPSSLOptions> GetAllChoices()
        {
            return Choices;
        }

        public RPSSLResult GetRoundResult(RPSSLOptions choiceIdEnum, RPSSLOptions oponentChoiceIdEnum)
        {
            RPSSLResult result = RPSSLResult.tie;
            if (choiceIdEnum == RPSSLOptions.spock)
            {
                if (oponentChoiceIdEnum == RPSSLOptions.scissors || oponentChoiceIdEnum == RPSSLOptions.rock)
                {
                    result = RPSSLResult.win;
                }
                else if (oponentChoiceIdEnum == RPSSLOptions.paper || oponentChoiceIdEnum == RPSSLOptions.lizard)
                {
                    result = RPSSLResult.lose;
                }
            }
            else if (choiceIdEnum == RPSSLOptions.scissors)
            {
                if (oponentChoiceIdEnum == RPSSLOptions.paper || oponentChoiceIdEnum == RPSSLOptions.lizard)
                {
                    result = RPSSLResult.win;
                }
                else if (oponentChoiceIdEnum == RPSSLOptions.spock || oponentChoiceIdEnum == RPSSLOptions.rock)
                {
                    result = RPSSLResult.lose;
                }
            }
            else if (choiceIdEnum == RPSSLOptions.paper)
            {
                if (oponentChoiceIdEnum == RPSSLOptions.spock || oponentChoiceIdEnum == RPSSLOptions.rock)
                {
                    result = RPSSLResult.win;
                }
                else if (oponentChoiceIdEnum == RPSSLOptions.lizard || oponentChoiceIdEnum == RPSSLOptions.scissors)
                {
                    result = RPSSLResult.lose;
                }
            }
            else if (choiceIdEnum == RPSSLOptions.lizard)
            {
                if (oponentChoiceIdEnum == RPSSLOptions.spock || oponentChoiceIdEnum == RPSSLOptions.paper)
                {
                    result = RPSSLResult.win;
                }
                else if (oponentChoiceIdEnum == RPSSLOptions.rock || oponentChoiceIdEnum == RPSSLOptions.scissors)
                {
                    result = RPSSLResult.lose;
                }
            }
            else if (choiceIdEnum == RPSSLOptions.rock)
            {
                if (oponentChoiceIdEnum == RPSSLOptions.lizard || oponentChoiceIdEnum == RPSSLOptions.scissors)
                {
                    result = RPSSLResult.win;
                }
                else if (oponentChoiceIdEnum == RPSSLOptions.spock || oponentChoiceIdEnum == RPSSLOptions.paper)
                {
                    result = RPSSLResult.lose;
                }
            }
            return result;
        }
    }
}
