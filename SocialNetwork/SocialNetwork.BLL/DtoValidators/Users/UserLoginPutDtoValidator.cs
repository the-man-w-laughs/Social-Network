using FluentValidation;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DtoValidators.Auth.Utils;

namespace SocialNetwork.BLL.DtoValidators.Users;

public class UserLoginPutDtoValidator : AbstractValidator<UserLoginPutDto>
{
    public UserLoginPutDtoValidator()
    {
        RuleFor(dto => dto.Login)
            .SetValidator(new UserLoginValidator());
    }
}

