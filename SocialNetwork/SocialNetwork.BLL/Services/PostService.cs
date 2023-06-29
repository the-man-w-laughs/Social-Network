using AutoMapper;
using DocumentFormat.OpenXml.Office2019.Word.Cid;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Comments;

namespace SocialNetwork.BLL.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;    
    private readonly ICommentRepository _commentRespository;
    private readonly IPostLikeRepository _postLikeRepository;

    public PostService(IMapper mapper, IPostRepository postRepository, ICommentRepository commentRespository, IPostLikeRepository postLikeRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _commentRespository = commentRespository;
        _postLikeRepository = postLikeRepository;
    }

    public Task<PostResponseDto> CreatePost(uint userId, PostRequestDto postRequestDto)
    {
        throw new NotImplementedException();
    }
    public async Task<Post> GetLocalPost(uint postId)
    {
        var post = await _postRepository.GetByIdAsync(postId) ??
            throw new NotFoundException($"Post (ID: {postId}) if not found."); ;
        return post;
    }

    public Task<PostResponseDto> GetPost(uint userId, object postRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<PostResponseDto> ChangePost(uint userId, PostPatchRequestDto postPatchRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<PostResponseDto> DeletePost(uint userId, uint postId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<CommentResponseDto>> GetComments(uint postId, int limit, int currCursor)
    {
        var post = await GetLocalPost(postId);   
        var paginatedCommentList = post.Comments.OrderBy(cm => cm.Id)
        .Skip(currCursor)
        .Take(limit)
        .ToList();
        return _mapper.Map<List<CommentResponseDto>>(paginatedCommentList);
    }
    private async Task<PostLike?> GetPostLike(uint userId, uint mediaId)
    {
        var result = await _postLikeRepository.GetAsync((postLike) => postLike.UserId == userId && postLike.PostId == mediaId);
        return result;
    }

    public async Task<PostLikeResponseDto> LikePost(uint userId, uint postId)
    {
        await GetLocalPost(postId);

        if (await GetPostLike(userId, postId) != null) throw new DuplicateEntryException("User already liked this post.");
        var newLike = await _postLikeRepository.LikeComment(userId, postId);
        return _mapper.Map<PostLikeResponseDto>(newLike);
    }

    public async Task<List<PostLikeResponseDto>> GetLikes(uint postId, int limit, int currCursor)
    {
        var post = await GetLocalPost(postId);

        var paginatedCommentLikesList = post.PostLikes.OrderBy(cm => cm.Id)
        .Skip(currCursor)
        .Take(limit)
        .ToList();
        return _mapper.Map<List<PostLikeResponseDto>>(paginatedCommentLikesList);
    }


    public async Task<PostLikeResponseDto> UnlikePost(uint userId, uint postId)
    {
        await GetLocalPost(postId);

        var postLIke = await GetPostLike(userId, postId);
        if (postLIke == null) throw new NotFoundException("User didn't like this post.");

        _postLikeRepository.Delete(postLIke);
        await _postLikeRepository.SaveAsync();
        return _mapper.Map<PostLikeResponseDto>(postLIke);
    }
}