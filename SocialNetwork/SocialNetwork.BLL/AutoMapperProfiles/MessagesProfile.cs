using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.AutoMapper;

public class MessagesProfile: BaseProfile
{
    public MessagesProfile()
    {
        CreateMap<MessageLike, MessageLikeResponseDto>();
        CreateMap<Message, MessageResponseDto>();
    }
}