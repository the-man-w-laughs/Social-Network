using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class UsersProfile: BaseProfile
{
    public UsersProfile()
    {
        CreateMap<UserLoginPutDto, User>();
        CreateMap<UserProfilePatchDto, UserProfile>();

        CreateMap<User, UserActivityResponseDto>();
        CreateMap<User, UserEmailResponseDto>();
        CreateMap<User, UserLoginResponseDto>();
        CreateMap<User, UserPasswordResponseDto>();
        CreateMap<UserProfile, UserProfileResponseDto>();
        CreateMap<User, UserResponseDto>();
    }
}