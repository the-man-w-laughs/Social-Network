using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;

namespace SocialNetwork.BLL.DtoValidators.Communities.Utils;

public class CommunityNameValidator : AbstractValidator<string>
{
    public CommunityNameValidator()
    {
        RuleFor(CommunityName => CommunityName)
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");            
    }
}