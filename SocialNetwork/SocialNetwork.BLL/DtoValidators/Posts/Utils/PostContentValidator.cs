using FluentValidation;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Posts.Utils;
public class PostContentValidator : AbstractValidator<string>
{
    public PostContentValidator()
    {
        RuleFor(content => content)
            .MaximumLength(Constants.PostContentMaxLength)
            .WithMessage($"Post content must be no more than {Constants.PostContentMaxLength} characters.");
    }
}