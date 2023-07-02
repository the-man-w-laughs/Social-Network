using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DtoValidators.Chats.Utils;

namespace SocialNetwork.BLL.DtoValidators.Chats;

public class ChatPatchDtoValidator : AbstractValidator<ChatPatchDto>
{
    public ChatPatchDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new ChatNameValidator())
            .When(dto => dto.Name != null);
    }
}