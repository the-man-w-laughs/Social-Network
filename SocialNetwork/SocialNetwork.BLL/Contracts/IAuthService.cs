using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Contracts;

public interface IAuthService
{
    Task<UserResponseDto> SignUpUser(UserSignUpRequestDto userSignUpRequestDto);
    Task<UserResponseDto> LoginUser(UserLoginRequestDto userLoginRequestDto);
    
}