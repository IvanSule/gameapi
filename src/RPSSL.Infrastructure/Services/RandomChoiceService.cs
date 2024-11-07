using RPSSL.Application.Abstractions;
using RPSSL.Domain.Enums;
using RPSSL.Domain.Exceptions;

namespace RPSSL.Domain.Implementations
{
    public class RandomChoiceService : IRandomChoiceService
    {
        private readonly IRandomNumberHttpService _randomNumberService;

        public RandomChoiceService(IRandomNumberHttpService randomNumberService)
        {
            _randomNumberService = randomNumberService;
        }

        public async Task<RPSSLOptions> GetRandomChoiceAsync()
        {
            var randomNumberResult = await _randomNumberService.GetRandomNumberResponseAsync();

            if (randomNumberResult == null || randomNumberResult.random_number <= 0 || randomNumberResult.random_number > 100) { throw new RandomNumberServiceException($"Boohma random number service returned unexpected value {randomNumberResult?.random_number}."); }

            int randomNumber = randomNumberResult.random_number;

            double choiceOptionInt = Math.Ceiling((double)randomNumber / 20);
            
            return (RPSSLOptions)choiceOptionInt;
        }
    }
}
