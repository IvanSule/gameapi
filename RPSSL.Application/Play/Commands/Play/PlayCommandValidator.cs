using FluentValidation;
using RPSSL.Domain.Const.ErrorCodes;

namespace RPSSL.Application.Play.Commands.Play
{
    public sealed class PlayCommandValidator : AbstractValidator<PlayCommand>
    {
        public PlayCommandValidator()
        {
            RuleFor(s => s.Player).NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5)
                .Configure(rule => rule.MessageBuilder = _ => PlayErrorCodes.PlayerChoiceIdInvalid);
        }
    }
}
