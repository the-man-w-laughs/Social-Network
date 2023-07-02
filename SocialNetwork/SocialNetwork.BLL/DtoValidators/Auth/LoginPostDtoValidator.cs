using FluentValidation;
using SocialNetwork.BLL.DTO.Auth.Request;

namespace SocialNetwork.BLL.DtoValidators.Auth;
public class LoginPostDtoValidator : AbstractValidator<LoginPostDto>
{
    public LoginPostDtoValidator()
    {
        RuleFor(dto => dto.Login)
            .NotEmpty().WithMessage("Login is required.");            

        RuleFor(dto => dto.Password)
            .NotEmpty().WithMessage("Password is required.");                   
    }
}