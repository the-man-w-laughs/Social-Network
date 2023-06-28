using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.AutoMapper;

public class UsersProfile: BaseProfile
{
    public UsersProfile()
    {
        CreateMap<UserLoginRequestDto, User>();
        CreateMap<UserProfilePatchRequestDto, UserProfile>();

        CreateMap<User, UserActivityResponseDto>();
        CreateMap<User, UserEmailResponseDto>();
        CreateMap<User, UserLoginResponseDto>();
        CreateMap<User, UserPasswordResponseDto>();
        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<User, UserResponseDto>();
    }
}