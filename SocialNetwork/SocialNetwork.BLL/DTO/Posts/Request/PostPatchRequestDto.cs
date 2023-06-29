namespace SocialNetwork.BLL.DTO.Posts.Request;

public class PostPatchRequestDto
{                    
    public string? Content { get; set; }
    public List<uint>? Attachments { get; set; } = new List<uint>();
}