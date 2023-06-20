using AutoMapper;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<User, UserResponseDto>();
        CreateMap<Chat, ChatResponseDto>();
        CreateMap<ChatMember, ChatMemberResponseDto>();
    }
}