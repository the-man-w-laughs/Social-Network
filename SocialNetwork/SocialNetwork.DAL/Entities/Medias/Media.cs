using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Medias;

public partial class Media
{
    public uint MediaId { get; set; }

    public string? EntityType { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public byte? MediaType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CommentMedia> CommentMedia { get; set; } = new List<CommentMedia>();

    public virtual MediaType? MediaTypeNavigation { get; set; }

    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();

    public virtual ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();

    public virtual ICollection<UserProfileMedia> UserProfileMedia { get; set; } = new List<UserProfileMedia>();
}
