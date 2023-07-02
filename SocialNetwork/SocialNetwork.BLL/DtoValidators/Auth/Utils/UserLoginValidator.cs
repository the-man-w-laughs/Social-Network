using FluentValidation;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Auth.Utils;
public class UserLoginValidator : AbstractValidator<string>
{
    public UserLoginValidator()
    {
        RuleFor(login => login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(Constants.UserLoginMinLength, Constants.UserLoginMaxLength)
            .WithMessage($"Login must be between {Constants.UserLoginMinLength} and {Constants.UserLoginMaxLength} characters.");
    }
}
