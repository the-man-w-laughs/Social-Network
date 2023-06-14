namespace SocialNetwork.DAL.Entities.Users;

public class UserProfile
{
    public int UserProfileId { get; set; }
    
    public int UserId { get; set; }
    
    public string UserEmail { get; set; }
    
    public string UserName { get; set; }
    
    public string UserSurname { get; set; }
    
    public string UserSex { get; set; }
    
    public string UserCountry { get; set; }
    
    public string UserEducation { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}