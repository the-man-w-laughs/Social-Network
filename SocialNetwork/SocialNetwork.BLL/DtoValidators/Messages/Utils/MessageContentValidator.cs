using FluentValidation;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Messages.Utils;
public class MessageContentValidator : AbstractValidator<string>
{
    public MessageContentValidator()
    {
        RuleFor(content => content)
            .MaximumLength(Constants.MessageContentMaxLength)
            .WithMessage($"Message content must be no more than {Constants.MessageContentMaxLength} characters.");
    }
}