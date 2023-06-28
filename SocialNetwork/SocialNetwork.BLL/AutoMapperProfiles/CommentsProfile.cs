using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.BLL.AutoMapper;

public class CommentsProfile : BaseProfile
{
    public CommentsProfile()
    {
        CreateMap<CommentLike, CommentLikeResponseDto>();
        CreateMap<Comment, CommentResponseDto>();
    }
}