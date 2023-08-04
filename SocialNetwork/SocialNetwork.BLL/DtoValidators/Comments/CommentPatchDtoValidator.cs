using FluentValidation;
using SocialNetwork.BLL.DTO.Comments.Request;

namespace SocialNetwork.BLL.DtoValidators.Comments;
public class CommentPatchDtoValidator : AbstractValidator<CommentPatchDto>
{
    public CommentPatchDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");
    }

    private bool HaveValidContentOrAttachments(CommentPatchDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}