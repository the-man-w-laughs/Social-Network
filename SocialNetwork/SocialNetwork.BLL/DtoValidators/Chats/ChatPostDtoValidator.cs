using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DtoValidators.Chats.Utils;

namespace SocialNetwork.BLL.DtoValidators.Chats;
public class ChatPostDtoValidator : AbstractValidator<ChatPostDto>
{
    public ChatPostDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .SetValidator(new ChatNameValidator());
    }
}