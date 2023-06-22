using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.DTO.Medias.Response;

public class CommunityMediaOwnerResponseDto
{
    public int Id { get; set; }
    public uint MediaId { get; set; }
    public uint CommunityId { get; set; }
}