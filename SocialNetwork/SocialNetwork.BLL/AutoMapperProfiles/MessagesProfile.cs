using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class MessagesProfile: BaseProfile
{
    public MessagesProfile()
    {
        CreateMap<MessageLike, MessageLikeResponseDto>();
        CreateMap<Message, MessageResponseDto>().ForMember(
            dto => dto.LikeCount, 
            expression => expression.MapFrom(message => message.MessageLikes.Count));
    }
}