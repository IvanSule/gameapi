using RPSSL.Domain.Enums;

namespace RPSSL.Application.Abstractions
{
    public interface IRandomChoiceService
    {
        Task<RPSSLOptions> GetRandomChoiceAsync();
    }
}
