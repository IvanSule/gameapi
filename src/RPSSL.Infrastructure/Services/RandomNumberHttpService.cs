using System.Net.Http.Json;
using RPSSL.Domain.Models;
using RPSSL.Application.Abstractions;

namespace RPSSL.Infrastructure.Services
{
    public class RandomNumberHttpService : IRandomNumberHttpService
    {
        private readonly HttpClient _httpClient;

        public RandomNumberHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BoohmaRandomNumberResponse?> GetRandomNumberResponseAsync()
        {
            var randomResponse = await _httpClient.GetFromJsonAsync<BoohmaRandomNumberResponse>("random");

            return randomResponse;
        }
    }
}
