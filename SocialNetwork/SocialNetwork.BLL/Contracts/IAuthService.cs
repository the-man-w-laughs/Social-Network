using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IAuthService
{
    Task<UserResponseDto> SignUp(SignUpRequestDto userSignUpRequestDto);
    Task<UserResponseDto> Login(LoginRequestDto userLoginRequestDto);
}