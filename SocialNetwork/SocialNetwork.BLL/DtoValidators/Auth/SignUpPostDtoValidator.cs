using FluentValidation;
using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DtoValidators.Auth.Utils;

namespace SocialNetwork.BLL.DtoValidators.Auth;
public class SignUpPostDtoValidator : AbstractValidator<SignUpPostDto>
{
    public SignUpPostDtoValidator()
    {
        RuleFor(dto => dto.Login)
            .SetValidator(new UserLoginValidator());

        RuleFor(dto => dto.Email)
            .SetValidator(new UserEmailValidator());

        RuleFor(dto => dto.Password)
            .SetValidator(new UserPasswordValidator());
    }
}