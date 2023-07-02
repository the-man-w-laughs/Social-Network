using FluentValidation;
using SocialNetwork.DAL;

public class UserEmailValidator : AbstractValidator<string>
{
    public UserEmailValidator()
    {
        RuleFor(Email => Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Length(Constants.UserEmailMinLength,Constants.UserEmailMaxLength)
            .WithMessage($"Email must be between {Constants.UserEmailMinLength} and {Constants.UserEmailMaxLength} characters.");
    }
}