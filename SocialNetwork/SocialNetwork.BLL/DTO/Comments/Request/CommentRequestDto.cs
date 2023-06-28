namespace SocialNetwork.BLL.DTO.Comments.Request;

public class CommentRequestDto
{
    public string? Content { get; set; }
    public List<uint>? Attachments { get; set; } = new List<uint>();    
}