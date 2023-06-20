using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.DTO.Chats.Response;

public class ChatMemberResponseDto
{        
    public uint Id { get; set; }
    public ChatMemberType TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }
    public uint ChatId { get; set; }
}