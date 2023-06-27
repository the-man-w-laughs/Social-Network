using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Communities.Request
{
    public class CommunityMemberRequestDto
    {
        public CommunityMemberType TypeId { get; set; }
    }
}
