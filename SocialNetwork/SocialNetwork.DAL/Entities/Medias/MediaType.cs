namespace SocialNetwork.DAL.Entities.Medias;

public partial class MediaType
{
    public byte MediaTypeId { get; set; }

    public string? MediaType1 { get; set; }

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();
}
