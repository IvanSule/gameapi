using MediatR;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Choices.Queries.GetChoices
{
    public sealed record GetChoicesQuery : IRequest<Result<IEnumerable<ChoiceResponse>>>;
}
