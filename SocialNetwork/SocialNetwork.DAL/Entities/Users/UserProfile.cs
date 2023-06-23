using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Users;

public partial class UserProfile
{
    public uint Id { get; set; }
    public uint? ProfilePictureId { get; set; }    
    public string? UserName { get; set; }
    public string? UserSurname { get; set; }
    public string? UserSex { get; set; }
    public string? UserCountry { get; set; }
    public string? UserEducation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }    

    public uint UserId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual Media? ProfilePicture { get; set; } = null;
    public virtual ICollection<UserProfileMedia> UserProfileMedia { get; set; } = new List<UserProfileMedia>();
}
