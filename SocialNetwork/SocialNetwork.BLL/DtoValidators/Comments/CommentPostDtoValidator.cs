using FluentValidation;
using SocialNetwork.BLL.DTO.Comments.Request;

namespace SocialNetwork.BLL.DtoValidators.Comments;
public class CommentPostDtoValidator : AbstractValidator<CommentPostDto>
{
    public CommentPostDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");
    }

    private bool HaveValidContentOrAttachments(CommentPostDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}