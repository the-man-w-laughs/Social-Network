namespace SocialNetwork.BLL.DTO.Users.Response;

public class UserEmailResponseDto
{
    public uint Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime EmailUpdatedAt { get; set; }
}