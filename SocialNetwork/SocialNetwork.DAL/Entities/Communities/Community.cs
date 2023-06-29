using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class Community
{
    public uint Id { get; set; }
    public uint? CommunityPictureId { get; set; }   
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Media? CommunityPicture { get; set; } = null;    
    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();
    public virtual ICollection<Post> CommunityPosts { get; set; } = new List<Post>();
}
