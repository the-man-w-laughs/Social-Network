using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;

namespace SocialNetwork.BLL.DtoValidators.Chats.Utils;

public class ChatNameValidator : AbstractValidator<string>
{
    public ChatNameValidator()
    {
        RuleFor(ChatName => ChatName)
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");            
    }
}