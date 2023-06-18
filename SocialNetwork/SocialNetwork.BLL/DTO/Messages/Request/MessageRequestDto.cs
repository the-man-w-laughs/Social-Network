using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Messages.Request
{
    public class MessageRequestDto
    {        
        public string? Content { get; set; }            
    }
}
