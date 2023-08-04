using FluentValidation;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Auth.Utils;
public class UserPasswordValidator : AbstractValidator<string>
{
    public UserPasswordValidator()
    {
        var userPasswordMinLength = Constants.UserPasswordMinLength;
        var userPasswordMaxLength = Constants.UserPasswordMaxLength;
        RuleFor(password => password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(userPasswordMinLength, userPasswordMaxLength)
            .WithMessage($"Password must be between {userPasswordMinLength} and {userPasswordMaxLength} characters.")
            .Must(BeStrongPassword)
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
    }

    private bool BeStrongPassword(string password)
    {
        var hasUpperCase = password.Any(char.IsUpper);
        var hasLowerCase = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasSpecialCharacter = password.Any(IsSpecialCharacter);

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialCharacter;
    }

    private bool IsSpecialCharacter(char c)
    {
        var specialCharacters = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
        return specialCharacters.Contains(c);
    }
}
