using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.CommentDto.Response
{
    public class CommentLikeResponseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public uint CommentId { get; set; }
        public uint UserId { get; set; }
    }
}
