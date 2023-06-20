namespace SocialNetwork.BLL.DTO.Users.Request;

public class UserSignUpRequestDto
{        
    public string? Email { get; set; }        
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}