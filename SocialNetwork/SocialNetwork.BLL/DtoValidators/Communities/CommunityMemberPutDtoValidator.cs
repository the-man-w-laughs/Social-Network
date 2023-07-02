using FluentValidation;
using SocialNetwork.BLL.DTO.Communities.Request;

namespace SocialNetwork.BLL.DtoValidators.Communities;
public class CommunityMemberPutDtoValidator : AbstractValidator<CommunityMemberPutDto>
{
    public CommunityMemberPutDtoValidator()
    {
        RuleFor(dto => dto.TypeId)
            .IsInEnum().WithMessage("Invalid community member type.");
    }
}

