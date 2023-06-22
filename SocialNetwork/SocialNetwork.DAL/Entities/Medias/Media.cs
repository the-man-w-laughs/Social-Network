using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Medias;

public partial class Media
{       
    public uint Id { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public MediaType MediaTypeId { get; set; }
    public OwnerType OwnerTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual UserMediaOwner UserMediaOwner { get; set; } = null!;
    public virtual CommunityMediaOwner CommunityMediaOwner { get; set; } = null!;
    public virtual ICollection<MediaLike> MediaLikes { get; set; } = new List<MediaLike>();
    public virtual ICollection<CommentMedia> CommentMedia { get; set; } = new List<CommentMedia>();
    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();
    public virtual ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();
    public virtual ICollection<UserProfileMedia> UserProfileMedia { get; set; } = new List<UserProfileMedia>();
}
