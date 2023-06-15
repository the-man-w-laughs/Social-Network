namespace SocialNetwork.DAL.Entities.Users;

public partial class UserType
{
    public byte UserTypeId { get; set; }

    public string? UserTypeName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
