namespace SocialNetwork.BLL.DTO.Users.Request;

public class UserPasswordPutDto
{
    public string PreviousPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
}