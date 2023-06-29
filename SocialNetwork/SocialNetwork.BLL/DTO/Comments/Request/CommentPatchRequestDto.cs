namespace SocialNetwork.BLL.DTO.Comments.Request;

public class CommentPatchRequestDto
{
    public string Content { get; set; }
    public List<uint>? Attachments { get; set; } = new List<uint>();
    public uint? RepliedCommentId { get; set; }
}