using FluentValidation;

namespace SocialNetwork.BLL.DtoValidators.Auth.Utils;
public class UserPasswordValidator : AbstractValidator<string>
{
    public UserPasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 100).WithMessage("Password must be between 8 and 100 characters.")
            .Must(BeStrongPassword).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
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
