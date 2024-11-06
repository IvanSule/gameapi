using RPSSL.Domain.Models;

namespace RPSSL.Application.Abstractions
{
    public interface IRandomNumberHttpService
    {
        Task<BoohmaRandomNumberResponse?> GetRandomNumberResponseAsync();
    }
}
