using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;

namespace SocialNetwork.BLL.DtoValidators.Communities.Utils;

public class CommunityDescriptionValidator : AbstractValidator<string>
{
    public CommunityDescriptionValidator()
    {
        RuleFor(CommunityName => CommunityName)
            .Length(1, 500).WithMessage("Description must be between 1 and 50 characters.");            
    }
}