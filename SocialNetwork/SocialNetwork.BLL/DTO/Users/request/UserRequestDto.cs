using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.request
{
    public class UserRequestDto
    {        
        public string? Email { get; set; }        
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
