using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.response
{
    public class UserEmailResponseDto
    {
        public uint Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime EmailUpdatedAt { get; set; }
    }
}
