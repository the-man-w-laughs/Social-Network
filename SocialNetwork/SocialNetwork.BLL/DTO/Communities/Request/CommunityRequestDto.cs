namespace SocialNetwork.BLL.DTO.Communities.Request;

public class CommunityRequestDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
}