using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Communities;

namespace SocialNetwork.BLL.Services;

public class CommunityService : ICommunityService
{
    private readonly IMapper _mapper;
    private readonly IMediaRepository _mediaRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityMemberRepository _communityMemberRepository;
    private readonly ICommunityPostRepository _communityPostRepository;
    private readonly IPostRepository _postRepository;

    public CommunityService(
        IMapper mapper,
        IMediaRepository mediaRepository,
        ICommunityRepository communityRepository, 
        ICommunityMemberRepository communityMemberRepository, 
        IPostRepository postRepository, 
        ICommunityPostRepository communityPostRepository)
    {
        _mapper = mapper;
        _mediaRepository = mediaRepository;
        _communityRepository = communityRepository;
        _communityMemberRepository = communityMemberRepository;
        _postRepository = postRepository;
        _communityPostRepository = communityPostRepository;
    }

    public async Task<CommunityResponseDto> AddCommunity(CommunityRequestDto communityRequestDto)
    {
        if (communityRequestDto.Name.Length == 0) throw new ArgumentException("Community name should have at east one character.");

        var community = _mapper.Map<Community>(communityRequestDto);
        community.CreatedAt = DateTime.Now;
        await _communityRepository.AddAsync(community);
        await _communityRepository.SaveAsync();
        return _mapper.Map<CommunityResponseDto>(community);
    }

    public async Task<CommunityMemberResponseDto> AddCommunityMember(uint communityId, uint userId, CommunityMemberType communityMemberType)
    {
        var communityMember = new CommunityMember()
        {
            CommunityId = communityId,
            CreatedAt = DateTime.Now,
            TypeId = communityMemberType,
            UserId = userId
        };        
        var addedCommunityMember = await _communityMemberRepository.AddAsync(communityMember);
        await _communityMemberRepository.SaveAsync();
        return _mapper.Map<CommunityMemberResponseDto>(addedCommunityMember);
    }

    public async Task<List<CommunityResponseDto>> GetCommunities(int limit, int currCursor)
    {
        var communities = await _communityRepository.GetAllAsync(c => !c.IsPrivate);
        var paginatedCommunities = communities!
            .OrderBy(c => c.Id)            
            .Skip(currCursor)
            .Take(limit)
            .ToList();
        return _mapper.Map<List<CommunityResponseDto>>(paginatedCommunities);
    }
    public async Task<CommunityMember?> GetCommunityMember(uint communityId, uint userId)
    {
        return await _communityMemberRepository.GetAsync(m => m.UserId == userId && m.CommunityId == communityId);
    }

    public async Task<CommunityMember> GetCommunityOwner(uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        return community!.CommunityMembers
            .First(cm => cm.TypeId == CommunityMemberType.Owner);
    }

    public async Task<CommunityResponseDto> DeleteCommunity(uint userId, uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ?? throw new NotFoundException("No community with this Id.");
        var communityMember = await GetCommunityMember(communityId, userId) ?? throw new NotFoundException("User is not community member.");
        if (communityMember.TypeId != CommunityMemberType.Owner) throw new OwnershipException("Only owner can delete community.");

        _communityRepository.Delete(community);
        await _communityRepository.SaveAsync();
        return _mapper.Map<CommunityResponseDto>(community);
    }

    public async Task<bool> IsUserCommunityMember(uint communityId, uint userId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        var communityMember = community?.CommunityMembers.FirstOrDefault(cm => cm.UserId == userId);
        return communityMember != null;
    }

    public async Task<CommunityPostResponseDto> AddCommunityPost(uint proposerId, uint communityId, PostRequestDto postRequestDto)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ?? throw new NotFoundException("No community with this Id.");
        var communityMember = await GetCommunityMember(communityId, proposerId);

        if (communityMember == null && community.IsPrivate) throw new OwnershipException("In private communities only members can propose posts.");

        var post = _mapper.Map<Post>(postRequestDto);
        post.CreatedAt = DateTime.Now;
        var addedPost = await _postRepository.AddAsync(post);
        await _postRepository.SaveAsync();

        var communityPost = new CommunityPost()
        {
            PostId = addedPost.Id,
            CommunityId = communityId,
            ProposerId = proposerId
        };

        await _communityPostRepository.AddAsync(communityPost);
        await _communityPostRepository.SaveAsync();

        return _mapper.Map<CommunityPostResponseDto>(communityPost);
    }

    public async Task<List<CommunityPostResponseDto>> GetCommunityPosts(uint userId, uint communityId, int limit, int currCursor)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ?? 
            throw new NotFoundException("No community with this Id.");
        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null && community.IsPrivate) 
            throw new OwnershipException("Only community members can get private community posts.");

        var posts = await _communityRepository.GetByIdAsync(communityId);
        var pagenatedPosts = posts!.CommunityPosts
            .OrderBy(cp => cp.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
        return _mapper.Map<List<CommunityPostResponseDto>>(pagenatedPosts); ;

    }
    public async Task<CommunityResponseDto> ChangeCommunity(uint userId, uint communityId, CommunityPatchRequestDto communityPatchRequestDto)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ?? throw new NotFoundException("No community with this Id.");

        var communityMember = await GetCommunityMember(communityId, userId) ?? throw new NotFoundException("User is not community member.");
        if (communityMember.TypeId == CommunityMemberType.Member) throw new OwnershipException("Member can't change community info.");

        bool updated = false;
        if (communityPatchRequestDto.CommunityPictureId != null)
        {
            var media = await _mediaRepository.GetByIdAsync((uint)communityPatchRequestDto.CommunityPictureId);
            if (media == null)
                throw new ArgumentException($"Media with id equal {communityPatchRequestDto.CommunityPictureId} doesn't exist.");
            else
            {
                if (community.CommunityPictureId != communityPatchRequestDto.CommunityPictureId)
                {
                community.CommunityPictureId = communityPatchRequestDto.CommunityPictureId;
                updated = true;
                }
            }
        }
        if (communityPatchRequestDto.Name != null)
        {
            if (communityPatchRequestDto.Name.Length == 0)
                throw new ArgumentException($"Community name should have at east one character.");
            else
            {
                if (community.Name != communityPatchRequestDto.Name)
                {
                    community.Name = communityPatchRequestDto.Name;
                    updated = true;
                }
            }
        }
        if (communityPatchRequestDto.Description != null)
        {
            if (communityPatchRequestDto.Description.Length == 0)
                throw new ArgumentException($"Community description should have at east one character.");
            else
            {
                if (community.Description != communityPatchRequestDto.Description)
                {
                    community.Description = communityPatchRequestDto.Description;
                    updated = true;
                }
            }
        }
        if (communityPatchRequestDto.IsPrivate != null)
        {
            if (community.IsPrivate != communityPatchRequestDto.IsPrivate)
            {
                community.Description = communityPatchRequestDto.Description;
                updated = true;
            }
        }
        if (updated)
        {
            community!.UpdatedAt = DateTime.Now;
            _communityRepository.Update(community!);
            await _communityRepository.SaveAsync();
        }
        return _mapper.Map<CommunityResponseDto>(community);
    }
}