using MediatR;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Choices.Queries.GetChoices
{
    public class GetChoicesQueryHandler : IRequestHandler<GetChoicesQuery, Result<IEnumerable<ChoiceResponse>>>
    {
        private readonly IPlayService _playService;

        public GetChoicesQueryHandler(IPlayService playService)
        {
            _playService = playService;
        }

        public Task<Result<IEnumerable<ChoiceResponse>>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
        {
            var choiceList = _playService.GetAllChoices().Select(item => new ChoiceResponse(item, item.ToString()));

            return Task.FromResult(Result.Success(choiceList));
        }
    }
}
