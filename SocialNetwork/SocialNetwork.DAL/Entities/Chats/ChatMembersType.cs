namespace SocialNetwork.DAL.Entities.Chats;

public partial class ChatMembersType
{
    public byte ChatMembersTypeId { get; set; }

    public string? ChatMembersTypeName { get; set; }

    public virtual ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
}
