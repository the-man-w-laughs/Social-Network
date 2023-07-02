using FluentValidation;

namespace SocialNetwork.BLL.DtoValidators.Auth.Utils;
public class UserLoginValidator : AbstractValidator<string>
{
    public UserLoginValidator()
    {
        RuleFor(login => login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.");
    }
}
