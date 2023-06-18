using AutoMapper;
using SocialNetwork.BLL.DTO;
using SocialNetwork.BLL.DTO.ChatDto.Response;
using SocialNetwork.BLL.DTO.Users.response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<Chat, ChatResponseDto>();
        CreateMap<ChatMember, ChatMemberResponseDto>();
    }
}