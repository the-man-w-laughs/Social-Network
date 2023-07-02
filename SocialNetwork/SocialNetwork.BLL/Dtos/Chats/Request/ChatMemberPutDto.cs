using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.DTO.Chats.Request;

public class ChatMemberPutDto
{
    public ChatMemberType Type { get; set; }
}