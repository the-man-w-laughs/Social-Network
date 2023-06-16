using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.ChatDto.Request
{
    public class PostChatMemberRequestDto
    {        
        public ChatMemberType TypeId { get; set; }        
    }
}
