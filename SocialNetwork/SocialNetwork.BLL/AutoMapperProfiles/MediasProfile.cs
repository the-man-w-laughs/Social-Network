using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.AutoMapper;

public class MediasProfile : BaseProfile
{
    public MediasProfile()
    {
        CreateMap<MediaLike, MediaLikeResponseDto>();
        CreateMap<Media, MediaResponseDto>();
    }
}