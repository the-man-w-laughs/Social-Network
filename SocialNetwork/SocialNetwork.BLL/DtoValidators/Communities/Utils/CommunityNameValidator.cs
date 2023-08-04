using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Communities.Utils;

public class CommunityNameValidator : AbstractValidator<string>
{
    public CommunityNameValidator()
    {
        RuleFor(CommunityName => CommunityName)
            .MaximumLength(Constants.CommunityNameMaxLength)
            .WithMessage($"Community name must be no more than {Constants.CommunityNameMaxLength} characters.");
    }
}