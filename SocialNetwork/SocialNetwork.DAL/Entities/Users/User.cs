using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DAL.Entities.Users;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    public string? Login { get; set; }
    
    public string? Password { get; set; }
    
    public string? Salt { get; set; }
    
    public byte UserTypeId { get; set; }
    
    public DateTime LastActiveAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTime DeletedAt { get; set; }
    
    public bool IsDeactivated { get; set; }
    
    public DateTime DeactivatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}