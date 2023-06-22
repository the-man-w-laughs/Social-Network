namespace SocialNetwork.BLL.DTO.Posts.Response;

public class PostLikeResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public uint PostId { get; set; }
    public uint UserId { get; set; }
}