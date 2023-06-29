using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Posts;

public partial class Post
{
    public uint Id { get; set; }
    public uint AuthorId { get; set; }
    public uint? CommunityId { get; set; }
    public bool? IsCommunityPost { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public uint? RepostId { get; set; }

    public virtual User Author { get; set; } = null!;
    public virtual Community? Community { get; set; }
    public virtual Post? Repost { get; set; }
    public virtual ICollection<Post> InverseRepost { get; set; } = new List<Post>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
    public virtual ICollection<Media> Attachments { get; set; } = new List<Media>();
}
