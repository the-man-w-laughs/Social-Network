using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Posts.Response
{
    public class PostLikeResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public uint PostId { get; set; }
        public uint UserId { get; set; }
    }
}
