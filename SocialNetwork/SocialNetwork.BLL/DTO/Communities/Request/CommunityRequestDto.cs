namespace SocialNetwork.BLL.DTO.Communities.Request;

public class CommunityRequestDto
{
    public string Name { get; set; } = null!;        
    public bool IsPrivate { get; set; }
}