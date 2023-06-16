using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.DTO;

public class UserDto
{
    public string Login { get; set; } = null!;
    public User.UserType TypeId { get; set; }
    public DateTime LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsDeactivated { get; set; }
}