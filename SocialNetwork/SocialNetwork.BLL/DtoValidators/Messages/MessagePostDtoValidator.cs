using FluentValidation;
using SocialNetwork.BLL.DTO.Messages.Request;

namespace SocialNetwork.BLL.DtoValidators.Messages;
public class MessagePostDtoValidator : AbstractValidator<MessagePostDto>
{
    public MessagePostDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");
    }

    private bool HaveValidContentOrAttachments(MessagePostDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}