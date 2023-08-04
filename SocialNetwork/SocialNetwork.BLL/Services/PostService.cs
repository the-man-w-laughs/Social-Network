using AutoMapper;
using DocumentFormat.OpenXml.Office2019.Word.Cid;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
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
    private readonly ICommunityRepository _communityRepository;
    private readonly IMediaRepository _mediaRepository;

    public PostService(
        IMapper mapper,
        IPostRepository postRepository,
        ICommentRepository commentRespository, 
        IPostLikeRepository postLikeRepository,
        ICommunityRepository communityRepository,
        IMediaRepository mediaRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _commentRespository = commentRespository;
        _postLikeRepository = postLikeRepository;
        _communityRepository = communityRepository;
        _mediaRepository = mediaRepository;
    }

    public async Task<PostResponseDto> CreatePost(uint userId, PostPostDto postRequestDto)
    {
        if (postRequestDto.CommunityId != null)
        {
            var community = await _communityRepository.GetByIdAsync((uint)postRequestDto.CommunityId);
            if (community == null)
                throw new NotFoundException("Community with this id is not found.");

            if (community.IsPrivate)
            {
                var member = community.CommunityMembers.FirstOrDefault(m => m.Id == userId);
                if (member == null)
                    throw new OwnershipException("In private communities only members can post.");
            }
        }
        else if (postRequestDto.IsCommunityPost != null && (bool)postRequestDto.IsCommunityPost)
            throw new NotFoundException("I can't post community post on your wall.");

        if (postRequestDto.RepostId != null)
        {
            var repliedPost = await _postRepository.GetByIdAsync((uint)postRequestDto.RepostId);
            if (repliedPost == null)
                throw new NotFoundException("Post with this id is not found.");
        }

        var newPost = new Post() {
            AuthorId = userId,
            CreatedAt = DateTime.Now,
            Content = postRequestDto.Content,
            RepostId = postRequestDto.RepostId,
            IsCommunityPost = postRequestDto.IsCommunityPost,
            CommunityId = postRequestDto.CommunityId
        };

        if (postRequestDto.Attachments != null)
        {
            foreach (var attachmentId in postRequestDto.Attachments)
            {
                var media = await _mediaRepository.GetByIdAsync(attachmentId);
                if (media != null)
                {
                    newPost.Attachments.Add(media);
                }
            }
        }

        if (newPost.Attachments.Count == 0 && string.IsNullOrWhiteSpace(newPost.Content))
            throw new ArgumentException("You can't make post without any attachments and content.");

        await _postRepository.AddAsync(newPost);
        await _postRepository.SaveAsync();
        var postResponseDto = _mapper.Map<PostResponseDto>(newPost);
        return postResponseDto;
    }
    public async Task<Post> GetLocalPost(uint postId)
    {
        var post = await _postRepository.GetByIdAsync(postId) ??
            throw new NotFoundException($"Post (ID: {postId}) if not found."); ;
        return post;
    }

    public async Task<PostResponseDto> GetPost(uint userId, uint postId)
    {        
        var post = await GetLocalPost(postId);

        if (userId != post.AuthorId)
        {
            if (post.CommunityId != null)
            {
                var community = await _communityRepository.GetByIdAsync((uint)post.CommunityId);

                if (community!.IsPrivate)
                {
                    var member = community.CommunityMembers.FirstOrDefault(m => m.Id == userId);
                    if (member == null)
                        throw new OwnershipException("In private communities only members can read posts.");
                }
            }
        }
        
        var postResponseDto = _mapper.Map<PostResponseDto>(post);
        postResponseDto.LikeCount = post.PostLikes.Count;
        return postResponseDto;
    }

    public async Task<PostResponseDto> ChangePost(uint userId, uint postId, PostPatchDto postPatchRequestDto)
    {
        var post = await GetLocalPost(postId);
        
        if (post.AuthorId != userId)
            throw new OwnershipException("Only post author can change it.");    

        bool updated = false;
        if (postPatchRequestDto.Content != null && postPatchRequestDto.Content != postPatchRequestDto.Content)
        {                                        
            postPatchRequestDto.Content = postPatchRequestDto.Content;
            updated = true;                
        }
        if (postPatchRequestDto.Attachments != null)
        {            
            post.Attachments.Clear();
            foreach (var attachmentId in postPatchRequestDto.Attachments)
            {
                var media = await _mediaRepository.GetByIdAsync(attachmentId);
                if (media != null)
                {
                    post.Attachments.Add(media);
                }
            }
            updated = true;
        }
        if (updated)
        {
            post!.UpdatedAt = DateTime.Now;
            _postRepository.Update(post!);
            await _postRepository.SaveAsync();
        }

        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<PostResponseDto> DeletePost(uint userId, uint postId)
    {
        var post = await GetLocalPost(postId);

        if (post.AuthorId != userId)
        {
            if (post.Community != null)
            {
                var member = post.Community.CommunityMembers.FirstOrDefault(m => m.UserId == userId);
                if (member != null)
                {
                    if (member.TypeId == CommunityMemberType.Member)
                        throw new OwnershipException("You are not post author.");
                }
                else throw new OwnershipException("You are not post author.");
            }
            else throw new OwnershipException("You are not post author.");
        }

        _postRepository.Delete(post);
        await _postRepository.SaveAsync();
        return _mapper.Map<PostResponseDto>(post);
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