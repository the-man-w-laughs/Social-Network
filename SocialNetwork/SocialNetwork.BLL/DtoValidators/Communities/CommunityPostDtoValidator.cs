using FluentValidation;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DtoValidators.Communities.Utils;

namespace SocialNetwork.BLL.DtoValidators.Communities;
public class CommunityPostDtoValidator : AbstractValidator<CommunityPostDto>
{
    public CommunityPostDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .SetValidator(new CommunityDescriptionValidator());       
    }
}