using FluentValidation;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.DAL;

namespace SocialNetwork.BLL.DtoValidators.Communities.Utils;

public class CommunityDescriptionValidator : AbstractValidator<string>
{
    public CommunityDescriptionValidator()
    {
        RuleFor(CommunityName => CommunityName)
            .MaximumLength(Constants.CommunityDescriptionMaxLength)
            .WithMessage($"Community description must be no more than {Constants.CommunityDescriptionMaxLength} characters.");
    }
}