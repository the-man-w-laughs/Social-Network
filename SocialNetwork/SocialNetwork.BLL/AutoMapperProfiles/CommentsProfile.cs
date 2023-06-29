using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class CommentsProfile : BaseProfile
{
    public CommentsProfile()
    {
        CreateMap<CommentLike, CommentLikeResponseDto>();
        CreateMap<Comment, CommentResponseDto>().ForMember(
            dto => dto.LikeCount, 
            expression => expression.MapFrom(comment => comment.CommentLikes.Count));
    }
}