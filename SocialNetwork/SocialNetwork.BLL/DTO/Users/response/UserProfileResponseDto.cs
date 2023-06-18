using SocialNetwork.DAL.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.response
{
    public class UserProfileResponseDto
    {
        public uint Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? UserSex { get; set; }
        public string? UserCountry { get; set; }
        public string? UserEducation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public uint UserId { get; set; }
    }
}
