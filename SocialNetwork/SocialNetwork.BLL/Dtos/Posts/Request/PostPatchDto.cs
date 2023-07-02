namespace SocialNetwork.BLL.DTO.Posts.Request;

public class PostPatchDto
{                    
    public string? Content { get; set; }
    public List<uint>? Attachments { get; set; } = new List<uint>();
}