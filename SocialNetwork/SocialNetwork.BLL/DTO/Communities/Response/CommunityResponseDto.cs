using SocialNetwork.DAL.Entities.Communities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Communities.Response
{
    public class CommunityResponseDto
    {
        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }        
    }
}
