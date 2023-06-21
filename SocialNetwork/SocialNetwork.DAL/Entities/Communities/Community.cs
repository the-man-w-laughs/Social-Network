using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class Community
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommunityMediaOwner> Medias { get; set; } = new List<CommunityMediaOwner>();
    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();
    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();
}
