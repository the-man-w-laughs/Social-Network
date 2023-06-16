using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.ChatDto.Request
{
    public class PostMessageRequestDto
    {
        public string? Content { get; set; }
        public uint? RepliedMessageId { get; set; }
    }
}
