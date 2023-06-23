namespace SocialNetwork.BLL.DTO.Comments.Response;

public class CommentResponseDto
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public uint? LikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public uint AuthorId { get; set; }
    public uint PostId { get; set; }
    public uint? RepliedComment { get; set; }
}