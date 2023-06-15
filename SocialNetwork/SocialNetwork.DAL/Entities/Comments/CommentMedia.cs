using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Comments;

public partial class CommentMedia
{
    public int CommentMediaId { get; set; }

    public uint CommentId { get; set; }

    public uint MediaId { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Media Media { get; set; } = null!;
}
