using MediatR;
using RPSSL.Application.Abstractions;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Choices.Queries.GetRandomChoice
{
    public class ChoicesQueryHandler : IRequestHandler<GetRandomChoiceQuery, Result<ChoiceResponse>>
    {
        private readonly IRandomChoiceService _choiceService;        

        public ChoicesQueryHandler(IRandomChoiceService choiceService)
        {
            _choiceService = choiceService;
        }

        public async Task<Result<ChoiceResponse>> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
        {
            var randomChoice = await _choiceService.GetRandomChoiceAsync();

            return Result.Success(new ChoiceResponse(randomChoice, randomChoice.ToString()));
        }
    }
}
