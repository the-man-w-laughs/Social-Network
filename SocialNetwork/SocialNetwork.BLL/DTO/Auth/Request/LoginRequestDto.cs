namespace SocialNetwork.BLL.DTO.Auth.Request;

public class LoginRequestDto
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}