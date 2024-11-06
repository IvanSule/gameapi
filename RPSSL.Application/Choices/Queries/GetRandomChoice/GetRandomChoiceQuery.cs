using MediatR;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Choices.Queries.GetRandomChoice
{
    public sealed record GetRandomChoiceQuery : IRequest<Result<ChoiceResponse>>;
}
