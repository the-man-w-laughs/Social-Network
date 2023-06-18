using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.response
{
    public class UserLoginResponseDto
    {
        public uint Id { get; set; }
        public string Login { get; set; } = null!;
        public DateTime LoginUpdatedAt { get; set; }
    }
}
