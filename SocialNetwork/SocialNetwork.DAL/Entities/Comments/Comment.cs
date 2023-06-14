namespace SocialNetwork.DAL.Entities.Comments;

public class Comment
{
    public int CommentId { get; set; }
    
    public int AuthorId { get; set; }
    
    public string Content { get; set; }
    
    public int PostId { get; set; }
    
    public int RepliedCommentId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}