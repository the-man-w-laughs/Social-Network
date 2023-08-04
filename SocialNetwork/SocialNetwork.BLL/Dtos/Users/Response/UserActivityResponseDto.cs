namespace SocialNetwork.BLL.DTO.Users.Response;

public class UserActivityResponseDto
{
    public uint Id { get; set; }
    public bool IsDeactivated { get; set; }
    public DateTime? DeactivatedAt { get; set; }
}