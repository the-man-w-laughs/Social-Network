namespace SocialNetwork.DAL.Entities.Comments;

public class CommentMedia
{
    public int CommentMediaId { get; set; }
    
    public int CommentId { get; set; }
    
    public int MediaId { get; set; }
}