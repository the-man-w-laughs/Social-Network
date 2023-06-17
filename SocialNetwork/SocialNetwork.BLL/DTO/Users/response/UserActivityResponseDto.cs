using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.response
{
    public class UserActivityResponseDto
    {
        public uint Id { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
