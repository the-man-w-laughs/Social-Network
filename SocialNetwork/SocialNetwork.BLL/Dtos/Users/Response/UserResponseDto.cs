using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.DTO.Users.Response;

public class UserResponseDto
{
    public uint Id { get; set; }

    public string? Email { get; set; }
    public DateTime? EmailUpdatedAt { get; set; }

    public string Login { get; set; } = null!;
    public DateTime? LoginUpdatedAt { get; set; }
    public DateTime? PasswordUpdatedAt { get; set; }

    public UserType TypeId { get; set; }
    public DateTime? UserTypeUpdatedAt { get; set; }
    public DateTime LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeactivated { get; set; }
    public DateTime? DeactivatedAt { get; set; }

}