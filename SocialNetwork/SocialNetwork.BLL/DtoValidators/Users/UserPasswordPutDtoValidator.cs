using FluentValidation;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DtoValidators.Auth.Utils;

namespace SocialNetwork.BLL.DtoValidators.Users;

public class UserPasswordPutDtoValidator : AbstractValidator<UserPasswordPutDto>
{
    public UserPasswordPutDtoValidator()
    {
        RuleFor(dto => dto.Password)
            .SetValidator(new UserPasswordValidator());
    }
}

