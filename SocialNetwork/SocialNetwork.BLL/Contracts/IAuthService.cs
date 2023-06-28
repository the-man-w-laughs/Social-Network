using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IAuthService
{
    Task<UserResponseDto> SignUpUser(SignUpRequestDto userSignUpRequestDto);
    Task<UserResponseDto> LoginUser(LoginRequestDto userLoginRequestDto);
    
}