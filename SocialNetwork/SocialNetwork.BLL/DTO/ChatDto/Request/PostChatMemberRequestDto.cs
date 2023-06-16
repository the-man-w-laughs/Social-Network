using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.ChatDto.Request
{
    public class PostChatMemberRequestDto
    {
        public enum RequestType { Member, Admin }
        public RequestType TypeId { get; set; }

        public uint UserId { get; set; }
    }
}
