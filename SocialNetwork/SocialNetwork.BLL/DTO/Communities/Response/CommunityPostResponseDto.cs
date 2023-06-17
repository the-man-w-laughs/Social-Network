using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Communities.Response
{
    public class CommunityPostResponseDto
    {
        public uint Id { get; set; }
        public uint CommunityId { get; set; }
        public uint PostId { get; set; }
        public uint? ProposerId { get; set; }
    }
}
