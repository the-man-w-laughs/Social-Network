using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.ChatDto.Response
{
    public class GetAllChatMessagesDto
    {
        public uint Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public uint ChatId { get; set; }
        public uint SenderId { get; set; }
        public uint? RepliedMessageId { get; set; }

    }
}
