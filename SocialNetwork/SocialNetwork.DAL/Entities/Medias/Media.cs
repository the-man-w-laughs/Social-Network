using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
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
    public uint OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }    
    
    public virtual User Owner { get; set; } = null!;
    public virtual ICollection<MediaLike> MediaLikes { get; set; } = new List<MediaLike>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();
    public virtual ICollection<PostMedia> PostMedia { get; set; } = new List<PostMedia>();
    public virtual ICollection<UserProfileMedia> UserProfileMedia { get; set; } = new List<UserProfileMedia>();
    public virtual ICollection<UserProfile> UserProfile { get; set; } = new List<UserProfile>();
    public virtual ICollection<Chat> Chat { get; set; } = new List<Chat>();
    public virtual ICollection<Community> Community { get; set; } = new List<Community>();
}
