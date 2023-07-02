namespace SocialNetwork.BLL.DTO.Auth.Request;

public class LoginPostDto
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}