using AutoMapper;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.DTO.ChatDto.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Chat, DeleteChatDto>();
        CreateMap<ChatMember, PostChatMemberResponseDto>();
    }
}