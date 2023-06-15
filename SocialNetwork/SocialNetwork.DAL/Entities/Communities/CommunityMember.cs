﻿using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class CommunityMember
{
    public enum Type {}
    
    public uint Id { get; set; }
    public Type TypeId { get; set; }
    public DateTime CreatedAt { get; set; }

    public uint UserId { get; set; }
    public uint CommunityId { get; set; }
    
    public virtual Community Community { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}