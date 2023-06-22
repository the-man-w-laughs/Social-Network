using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Entities.Users;

public partial class User
{        
    public uint Id { get; set; }

    public string? Email { get; set; }
    public DateTime? EmailUpdatedAt { get; set; }

    public string Login { get; set; } = null!;
    public DateTime? LoginUpdatedAt { get; set; }

    public byte[] PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public DateTime? PasswordUpdatedAt { get; set; }

    public UserType TypeId { get; set; }
    public DateTime? UserTypeUpdatedAt { get; set; }
    public DateTime LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeactivated { get; set; }
    public DateTime? DeactivatedAt { get; set; }      
    
    public virtual ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();
    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();
    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
    public virtual ICollection<UserFollower> UserFollowerSources { get; set; } = new List<UserFollower>();
    public virtual ICollection<UserFollower> UserFollowerTargets { get; set; } = new List<UserFollower>();
    public virtual ICollection<UserFriend> UserFriendUser1s { get; set; } = new List<UserFriend>();
    public virtual ICollection<UserFriend> UserFriendUser2s { get; set; } = new List<UserFriend>();
    public virtual ICollection<UserProfilePost> UserProfilePosts { get; set; } = new List<UserProfilePost>();
    public virtual UserProfile UserProfile { get; set; } = new UserProfile();
}
