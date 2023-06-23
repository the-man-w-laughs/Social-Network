namespace SocialNetwork.BLL.DTO.Communities.Request;

public class CommunityPatchRequestDto
{
    public string? Name { get; set; } = null!;
    public uint? CommunityPictureId { get; set; }
    public string? Description { get; set; }
    public bool? IsPrivate { get; set; }
}