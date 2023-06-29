using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IAdminService
{
    Task<CommunityResponseDto> DeleteCommunity(uint communityId);
    Task<UserResponseDto> DeleteUser(uint userId);
    Task<PostResponseDto> DeletePost(uint postId);
    Task<CommentResponseDto> DeleteComment(uint commentId);
}