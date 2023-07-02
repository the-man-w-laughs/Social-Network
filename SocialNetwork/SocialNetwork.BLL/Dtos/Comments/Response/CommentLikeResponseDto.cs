namespace SocialNetwork.BLL.DTO.Comments.Response;

public class CommentLikeResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public uint CommentId { get; set; }
    public uint UserId { get; set; }
}