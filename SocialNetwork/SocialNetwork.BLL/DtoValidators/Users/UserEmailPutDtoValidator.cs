using FluentValidation;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DtoValidators.Auth.Utils;

namespace SocialNetwork.BLL.DtoValidators.Users;

public class UserEmailPutDtoValidator : AbstractValidator<UserEmailPutDto>
{
    public UserEmailPutDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .SetValidator(new UserEmailValidator());
    }
}

