using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class PostsProfile : BaseProfile
{
    public PostsProfile()
    {
        CreateMap<PostRequestDto, Post>();

        CreateMap<PostLike, PostLikeResponseDto>();
        CreateMap<Post, PostResponseDto>().ForMember(
            dto => dto.LikeCount, 
            expression => expression.MapFrom(post => post.PostLikes.Count));                
    }
}