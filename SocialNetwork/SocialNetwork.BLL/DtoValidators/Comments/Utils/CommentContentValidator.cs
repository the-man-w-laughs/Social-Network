using FluentValidation;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Comments.Utils;
public class CommentContentValidator : AbstractValidator<String>
{
    public CommentContentValidator()
    {
        RuleFor(content => content)
            .MaximumLength(Constants.CommentContentMexLength)
            .WithMessage($"Comment content must be no more than {Constants.CommentContentMexLength} characters.");
    }
}