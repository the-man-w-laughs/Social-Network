namespace SocialNetwork.BLL.DTO.Users.Response;

public class UserLoginResponseDto
{
    public uint Id { get; set; }
    public string Login { get; set; } = null!;
    public DateTime LoginUpdatedAt { get; set; }
}