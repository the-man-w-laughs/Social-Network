using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.AutoMapper;

public class ChatsProfile : BaseProfile
{
    public ChatsProfile()
    {        
        CreateMap<ChatRequestDto, Chat>();
        CreateMap<ChatMemberRequestDto, Chat>();

        CreateMap<Chat, ChatResponseDto>();
        CreateMap<ChatMember, ChatMemberResponseDto>();        
    }
}