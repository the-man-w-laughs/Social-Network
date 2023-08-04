using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Chats.Utils;

public class ChatNameValidator : AbstractValidator<string>
{
    public ChatNameValidator()
    {
        RuleFor(ChatName => ChatName)
            .MaximumLength(Constants.ChatNameMaxLength)
            .WithMessage($"Chat name must be no more than {Constants.ChatNameMaxLength} characters.");
    }
}