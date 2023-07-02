using FluentValidation;
using SocialNetwork.BLL.DTO.Posts.Request;

namespace SocialNetwork.BLL.DtoValidators.Posts;

public class PostPostDtoValidator : AbstractValidator<PostPostDto>
{
    public PostPostDtoValidator()
    {
        RuleFor(dto => dto)
            .Must(HaveValidContentOrAttachments)
            .WithMessage("You must provide either content, attachments, or both.");
    }

    private bool HaveValidContentOrAttachments(PostPostDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Content) || (dto.Attachments != null && dto.Attachments.Count > 0);
    }
}