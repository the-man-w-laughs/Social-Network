namespace SocialNetwork.DAL.Entities.Users;

public sealed class UserProfile
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
    
    public User User { get; set; } = null!;
    public ICollection<UserProfileMedia> UserProfileMedia { get; set; } = new List<UserProfileMedia>();
}
