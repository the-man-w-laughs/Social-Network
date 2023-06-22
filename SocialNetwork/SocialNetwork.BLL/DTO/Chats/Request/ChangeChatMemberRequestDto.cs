using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.DTO.Chats.Request;

public class ChangeChatMemberRequestDto
{
    public ChatMemberType Type { get; set; }
}