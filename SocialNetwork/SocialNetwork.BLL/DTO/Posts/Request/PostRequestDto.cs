namespace SocialNetwork.BLL.DTO.Posts.Request;

public class PostRequestDto
{
    public string Content { get; set; }    
    public List<uint> Attachments { get; set; } = new List<uint>();
    public uint RepostId { get; set; }
}