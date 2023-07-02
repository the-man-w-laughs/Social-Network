using FluentValidation;
using SocialNetwork.BLL.DTO.Users.Request;

namespace SocialNetwork.BLL.DTO.Users.Validators
{
    public class UserProfilePatchRequestDtoValidator : AbstractValidator<UserProfilePatchDto>
    {
        public UserProfilePatchRequestDtoValidator()
        {
            RuleFor(dto => dto.UserName)
                .Cascade(CascadeMode.Stop)                
                .Length(1, 50).WithMessage("User name should be between 1 and 50 characters.")
                .When(dto => dto.UserName != null);

            RuleFor(dto => dto.UserSurname)
                .Cascade(CascadeMode.Stop)                
                .Length(1, 50).WithMessage("User surname should be between 1 and 50 characters.")
                .When(dto => dto.UserSurname != null);

            RuleFor(dto => dto.UserSex)
                .Cascade(CascadeMode.Stop)
                .Length(1, 50).WithMessage("User sex should be between 1 and 50 characters.")
                .When(dto => dto.UserSex != null);

            RuleFor(dto => dto.UserCountry)
                .Cascade(CascadeMode.Stop)                
                .Length(1, 50).WithMessage("User country should be between 1 and 50 characters.")
                .When(dto => dto.UserCountry != null);

            RuleFor(dto => dto.UserEducation)
                .Cascade(CascadeMode.Stop)
                .Length(1, 50).WithMessage("User education should be between 1 and 50 characters.")
                .When(dto => dto.UserEducation != null);
        }
    }
}