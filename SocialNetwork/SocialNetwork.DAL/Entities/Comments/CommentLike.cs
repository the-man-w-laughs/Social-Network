namespace SocialNetwork.DAL.Entities.Comments;

public class CommentLike
{
    public int CommentLikeId { get; set; }
    
    public int CommentId { get; set; }
    
    public int AuthorId { get; set; }
}