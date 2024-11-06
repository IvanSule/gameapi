using MediatR;
using RPSSL.Domain.Models;

namespace RPSSL.Application.Play.Commands.Play
{
    public sealed record PlayCommand(int Player) : IRequest<Result<PlayResponse>>;
}
