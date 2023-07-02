using FluentValidation;
using SocialNetwork.BLL.DTO.Messages.Request;

namespace SocialNetwork.BLL.DtoValidators.Messages;
public class MessagePatchDtoValidator : AbstractValidator<MessagePatchDto>
{
    public MessagePatchDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");
    }

    private bool HaveValidContentOrAttachments(MessagePatchDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}