using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;

namespace SocialNetwork.BLL.Services;

public class AdminService : IAdminService
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public AdminService(IMapper mapper, ICommunityRepository communityRepository, IUserRepository userRepository, 
        IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<CommunityResponseDto> DeleteCommunity(uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        if (community == null)
            throw new NotFoundException("Community doesn't exist");
        await _communityRepository.DeleteById(communityId);
        await _communityRepository.SaveAsync();
        return _mapper.Map<CommunityResponseDto>(community);
    }

    public async Task<UserResponseDto> DeleteUser(uint userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("User doesn't exist");
        await _userRepository.DeleteById(userId);
        await _userRepository.SaveAsync();
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<PostResponseDto> DeletePost(uint postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null)
            throw new NotFoundException("Pos doesn't exist");
        await _postRepository.DeleteById(postId);
        await _postRepository.SaveAsync();
        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<CommentResponseDto> DeleteComment(uint commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null)
            throw new NotFoundException("Comment doesn't exist");
        await _commentRepository.DeleteById(commentId);
        await _commentRepository.SaveAsync();
        return _mapper.Map<CommentResponseDto>(comment);
    }
}