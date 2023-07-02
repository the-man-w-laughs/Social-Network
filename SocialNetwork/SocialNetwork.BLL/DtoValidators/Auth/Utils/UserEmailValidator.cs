using FluentValidation;

namespace SocialNetwork.BLL.DtoValidators.Auth.Utils;
public class UserEmailValidator : AbstractValidator<string>
{
    public UserEmailValidator()
    {
        RuleFor(Email => Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}