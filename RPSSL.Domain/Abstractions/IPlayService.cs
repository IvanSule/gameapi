using RPSSL.Domain.Enums;

namespace RPSSL.Domain.Abstractions
{
    public interface IPlayService
    {
        RPSSLResult GetRoundResult(RPSSLOptions choiceIdEnum, RPSSLOptions oponentChoiceIdEnum);
        IEnumerable<RPSSLOptions> GetAllChoices();
    }
}
