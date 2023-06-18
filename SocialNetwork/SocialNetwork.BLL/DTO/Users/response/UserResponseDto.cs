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
    public class UserResponseDto
    {
        public uint Id { get; set; }

        public string? Email { get; set; }
        public DateTime EmailUpdatedAt { get; set; }

        public string Login { get; set; } = null!;
        public DateTime LoginUpdatedAt { get; set; }

        public byte[] PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public DateTime PasswordUpdatedAt { get; set; }

        public UserType TypeId { get; set; }
        public DateTime UserTypeUpdatedAt { get; set; }
        public DateTime LastActiveAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeactivated { get; set; }
        public DateTime? DeactivatedAt { get; set; }

    }
}
