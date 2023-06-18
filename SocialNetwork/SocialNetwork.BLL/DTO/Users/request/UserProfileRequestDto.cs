using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DTO.Users.request
{
    public class UserProfileRequestDto
    {                
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
