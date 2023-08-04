using FluentValidation;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DtoValidators.Communities.Utils;

namespace SocialNetwork.BLL.DtoValidators.Communities;

public class CommunityPatchDtoValidator : AbstractValidator<CommunityPatchDto>
{
    public CommunityPatchDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new CommunityNameValidator())
            .When(dto => dto.Name != null);

        RuleFor(dto => dto.Description)
            .Cascade(CascadeMode.Stop)
            .SetValidator(new CommunityDescriptionValidator())
            .When(dto => dto.Description != null);
    }
}