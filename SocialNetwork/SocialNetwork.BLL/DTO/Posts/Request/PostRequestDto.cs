namespace SocialNetwork.BLL.DTO.Posts.Request;

public class PostRequestDto
{        
    public uint? RepostId { get; set; }
    public uint? CommunityId { get; set; }
    public bool IsCommunityPost { get; set; }
    public string? Content { get; set; }
    public List<uint>? Attachments { get; set; } = new List<uint>();
}