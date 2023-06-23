namespace SocialNetwork.BLL.DTO.Users.Request;

public class UserProfilePatchRequestDto
{
    public uint? ProfilePictureId { get; set; }
    public string? UserName { get; set; }    
    public string? UserSurname { get; set; }
    public string? UserSex { get; set; }
    public string? UserCountry { get; set; }
    public string? UserEducation { get; set; }
}