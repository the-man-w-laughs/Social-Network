using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.request
{
    public class UserPasswordRequestDto
    {
        public string PreviousPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
