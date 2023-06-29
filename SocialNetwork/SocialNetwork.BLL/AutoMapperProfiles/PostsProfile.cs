using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.AutoMapper;

public class PostsProfile : BaseProfile
{
    public PostsProfile()
    {
        CreateMap<PostRequestDto, Post>();

        CreateMap<PostLike, PostLikeResponseDto>();
        CreateMap<Post, PostResponseDto>();                
    }
}