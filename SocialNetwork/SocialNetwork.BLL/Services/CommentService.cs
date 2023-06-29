using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Medias;
using Comment = SocialNetwork.DAL.Entities.Comments.Comment;

namespace SocialNetwork.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IPostRepository _postRepository;
    private readonly ICommentLikeRepository _commentLikeRepository;
    public CommentService(
        IMapper mapper,
        ICommentRepository commentRepository,
        IMediaRepository mediaRepository,
        IPostRepository postRepository,
        ICommentLikeRepository commentLikeRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _mediaRepository = mediaRepository;
        _commentLikeRepository = commentLikeRepository;
    }

    public async Task<CommentResponseDto> AddComment(uint userId, CommentRequestDto commentRequestDto)
    {
        var post = await _postRepository.GetByIdAsync(commentRequestDto.PostId);
        if (post == null)
            throw new NotFoundException("Post with this id is not found.");
        
        if (commentRequestDto.RepliedCommentId != null)
        {
            var repliedComment = await _commentRepository.GetByIdAsync((uint)commentRequestDto.RepliedCommentId);
            if (repliedComment == null)
                throw new NotFoundException("Comment with this id is not found.");
        }

        var newComment = _mapper.Map<Comment>(commentRequestDto);
        newComment.AuthorId = userId;
        newComment.CreatedAt = DateTime.Now;

        await _commentRepository.AddAsync(newComment);                

        if (commentRequestDto.Attachments != null)
        {
            foreach (var attachmentId in commentRequestDto.Attachments)
            {
                var media = await _mediaRepository.GetByIdAsync(attachmentId);
                if (media != null)
                {
                    newComment.Attachments.Add(media);                    
                }
            }
        }

        if (newComment.Attachments.Count == 0 && string.IsNullOrWhiteSpace(commentRequestDto.Content))
            throw new ArgumentException("You can't make comment without any attachments and content.");

        await _commentRepository.SaveAsync();
        var commentResponseDto = _mapper.Map<CommentResponseDto>(newComment);
        return commentResponseDto;
    }

    public async Task<CommentResponseDto> GetComment(uint commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null)
            throw new NotFoundException("Comment with this id is not found.");

        var commentResponseDto = _mapper.Map<CommentResponseDto>(comment);
        commentResponseDto.LikeCount = (uint)comment.CommentLikes.Count;
        return commentResponseDto;
    }
    
    public async Task<CommentResponseDto> ChangeComment(uint userId, uint commentId, CommentPatchRequestDto commentPatchRequestDto)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId) ?? throw new NotFoundException("No comment with this Id.");
        if (comment.Author.Id != userId)
            throw new OwnershipException("Only comment owner can change it.");

        bool updated = false;
        if (commentPatchRequestDto.Content != null)
        {
            if (commentPatchRequestDto.Content.Length == 0)
                throw new ArgumentException($"Content should have at least 1 character without whitespaces.");
            else
            {
                if (comment.Content != commentPatchRequestDto.Content)
                {
                    comment.Content = commentPatchRequestDto.Content;
                    updated = true;
                }
            }
        }
        if (commentPatchRequestDto.Attachments != null)
        {
            var newMediaList = new List<Media>();
            comment.Attachments.Clear();
            foreach (var attachmentId in commentPatchRequestDto.Attachments)
            {
                var media = await _mediaRepository.GetByIdAsync(attachmentId);
                if (media != null)
                {
                    comment.Attachments.Add(media);                
                }
            }            
            updated = true;
        }
        if (updated)
        {
            comment!.UpdatedAt = DateTime.Now;
            _commentRepository.Update(comment!);
            await _commentRepository.SaveAsync();
        }
        return _mapper.Map<CommentResponseDto>(comment);
    }

    public async Task<CommentResponseDto> DeleteComment(uint userId, uint commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment == null)
            throw new NotFoundException("Comment with this id is not found.");
        if (comment.AuthorId != userId)
            throw new OwnershipException("Only owner can delete comment.");

        _commentRepository.Delete(comment);
        await _commentRepository.SaveAsync();

        var commentResponseDto = _mapper.Map<CommentResponseDto>(comment);
        return commentResponseDto;
    }

    public async Task<CommentLikeResponseDto> LikeComment(uint userId, uint commentId)
    {
        if (await IsUserLiked(userId, commentId)) throw new DuplicateEntryException("User already liked this comment.");
        var newLike = await _commentLikeRepository.LikeComment(userId, commentId);
        return _mapper.Map<CommentLikeResponseDto>(newLike);
    }

    private async Task<bool> IsUserLiked(uint userId, uint mediaId)
    {
        var result = await _commentLikeRepository.GetAsync((mediaLike) => mediaLike.UserId == userId && mediaLike.CommentId == mediaId);
        if (result != null) return true; else return false;
    }

    public async Task<List<CommentLikeResponseDto>> GetCommentLikes(uint commentId, int limit, int currCursor)
    {
        var commentLikesList = await _commentLikeRepository.GetCommentLikes(commentId);
        var paginatedCommentLikesList = commentLikesList.OrderBy(cm => cm.Id)
        .Skip(currCursor)
        .Take(limit)
        .ToList();
        return _mapper.Map<List<CommentLikeResponseDto>>(paginatedCommentLikesList);
    }

    public async Task<CommentLikeResponseDto> UnlikeComment(uint userId, uint commentId)
    {
        if (!await IsUserLiked(userId, commentId)) throw new NotFoundException("User didn't like this comment.");
        var mediaLike = await _commentLikeRepository.UnLikeComment(userId, commentId);
        return _mapper.Map<CommentLikeResponseDto>(mediaLike);
    }
}