using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.CommentDto.Response
{
    public class CommentResponseDto
    {
        public uint Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public uint AuthorId { get; set; }
        public uint PostId { get; set; }
        public uint? RepliedComment { get; set; }
    }
}
