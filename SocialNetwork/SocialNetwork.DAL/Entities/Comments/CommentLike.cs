using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Comments;

public partial class CommentLike
{
    public int Id { get; set; }
    
    public uint CommentId { get; set; }
    public uint UserId { get; set; }

    public virtual Comment Comment { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
