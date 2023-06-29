using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IAdminService
{
    Task<MediaResponseDto> DeleteMedia(uint mediaId);
    Task<CommunityResponseDto> DeleteCommunity(uint communityId);
    Task<UserResponseDto> DeleteUser(uint userId);
    Task<PostResponseDto> DeletePost(uint postId);
    Task<CommentResponseDto> DeleteComment(uint commentId);
}