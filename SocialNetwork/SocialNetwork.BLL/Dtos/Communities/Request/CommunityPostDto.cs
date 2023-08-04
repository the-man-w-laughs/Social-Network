namespace SocialNetwork.BLL.DTO.Communities.Request;

public class CommunityPostDto
{
    public string Name { get; set; } = null!;        
    public bool IsPrivate { get; set; }
}