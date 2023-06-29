using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class MediasProfile : BaseProfile
{
    public MediasProfile()
    {
        CreateMap<MediaLike, MediaLikeResponseDto>();
        CreateMap<Media, MediaResponseDto>().ForMember(
            dto => dto.LikeCount, 
            expression => expression.MapFrom(media => media.MediaLikes.Count));
    }
}