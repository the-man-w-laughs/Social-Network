namespace SocialNetwork.BLL.DTO.Auth.Request;

public class SignUpRequestDto
{        
    public string Email { get; set; }        
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}