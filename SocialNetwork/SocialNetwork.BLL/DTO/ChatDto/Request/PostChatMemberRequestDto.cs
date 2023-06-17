using SocialNetwork.DAL.Entities.Enums;

namespace SocialNetwork.BLL.DTO.ChatDto.Request;

public class PostChatMemberRequestDto
{        
    public ChatMemberType TypeId { get; set; }        
}