namespace SocialNetwork.DAL.Entities.Medias;

public class Media
{
    public int MediaId { get; set; }
    
    public string FileName { get; set; }
    
    public string FilePath { get; set; }
    
    public byte MediaTypeId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}