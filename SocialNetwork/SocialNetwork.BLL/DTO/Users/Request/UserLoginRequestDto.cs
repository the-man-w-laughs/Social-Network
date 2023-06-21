namespace SocialNetwork.BLL.DTO.Users.Request;

public class UserLoginRequestDto
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}