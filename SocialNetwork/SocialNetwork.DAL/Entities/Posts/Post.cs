using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Posts;

public partial class Post
{
    public uint PostId { get; set; }

    public string? Content { get; set; }

    public uint RepostId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UpdatedAt { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();

    public virtual ICollection<Post> InverseRepost { get; set; } = new List<Post>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();

    public virtual Post Repost { get; set; } = null!;

    public virtual ICollection<UserProfilePost> UserProfilePosts { get; set; } = new List<UserProfilePost>();
}
