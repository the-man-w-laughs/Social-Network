using FluentValidation;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DTO.Users.Validators
{
    public class UserProfilePatchRequestDtoValidator : AbstractValidator<UserProfilePatchDto>
    {
        public UserProfilePatchRequestDtoValidator()
        {
            RuleFor(dto => dto.UserName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.UserNameMaxLength)
                .WithMessage($"User name must be no more than {Constants.UserNameMaxLength} characters.")
                .When(dto => dto.UserName != null);

            RuleFor(dto => dto.UserSurname)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.UserSurnameMaxLength)
                .WithMessage($"User surname must be no more than {Constants.UserSurnameMaxLength} characters.")
                .When(dto => dto.UserSurname != null);

            RuleFor(dto => dto.UserSex)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.UserSexMaxLength)
                .WithMessage($"Sex must be no more than {Constants.UserSexMaxLength} characters.")
                .When(dto => dto.UserSex != null);

            RuleFor(dto => dto.UserCountry)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.CountryNameMaxLength)
                .WithMessage($"Country name must be no more than {Constants.CountryNameMaxLength} characters.")
                .When(dto => dto.UserCountry != null);

            RuleFor(dto => dto.UserEducation)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.UserEducationMaxLength)
                .WithMessage($"Education field must be no more than {Constants.UserEducationMaxLength} characters.")
                .When(dto => dto.UserEducation != null);
        }
    }
}