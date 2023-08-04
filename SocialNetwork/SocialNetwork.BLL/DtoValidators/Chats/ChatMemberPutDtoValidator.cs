using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.DtoValidators.Chats;

public class ChatMemberPutDtoValidator : AbstractValidator<ChatMemberPutDto>
{
    public ChatMemberPutDtoValidator()
    {
        RuleFor(dto => dto.Type)
            .IsInEnum().WithMessage("Invalid chat member type.");
    }
}