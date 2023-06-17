using SocialNetwork.DAL.Entities.Enums;

namespace SocialNetwork.BLL.DTO.ChatDto.Response;

public class PostChatMemberResponseDto
{        
    public uint Id { get; set; }
    public ChatMemberType TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }
    public uint ChatId { get; set; }
}