using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class ChatsProfile : BaseProfile
{
    public ChatsProfile()
    {
        CreateMap<ChatRequestDto, Chat>();
        CreateMap<ChatMemberRequestDto, Chat>();

        CreateMap<Chat, ChatResponseDto>().ForMember(
            dto => dto.UserCount,
            expression => expression.MapFrom(chat => chat.ChatMembers.Count));
        
        CreateMap<ChatMember, ChatMemberResponseDto>();        
    }
}