using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class CommunityPost
{
    public uint CommunityPostId { get; set; }

    public uint? CommunityId { get; set; }

    public uint? PostId { get; set; }

    public uint? ProposerId { get; set; }

    public virtual Community? Community { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? Proposer { get; set; }
}
