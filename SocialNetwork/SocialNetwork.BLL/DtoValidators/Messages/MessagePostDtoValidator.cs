using FluentValidation;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DtoValidators.Comments;
using SocialNetwork.BLL.DtoValidators.Messages.Utils;

namespace SocialNetwork.BLL.DtoValidators.Messages;
public class MessagePostDtoValidator : AbstractValidator<MessagePostDto>
{
    public MessagePostDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");

        RuleFor(dto => dto.Content).Cascade(CascadeMode.Stop)
            .SetValidator(new MessageContentValidator())
            .When(dto => dto.Content != null);
    }

    private bool HaveValidContentOrAttachments(MessagePostDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}