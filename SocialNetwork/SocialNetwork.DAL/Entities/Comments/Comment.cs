using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Comments;

public partial class Comment
{
    public uint CommentId { get; set; }

    public uint AuthorId { get; set; }

    public string? Content { get; set; }

    public uint PostId { get; set; }

    public uint? RepliedComment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual ICollection<CommentMedia> CommentMedia { get; set; } = new List<CommentMedia>();

    public virtual ICollection<Comment> InverseRepliedCommentNavigation { get; set; } = new List<Comment>();

    public virtual Post Post { get; set; } = null!;

    public virtual Comment? RepliedCommentNavigation { get; set; }
}
