using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class CommunityService : ICommunityService
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityMemberRepository _communityMemberRepository;
    private readonly ICommunityPostRepository _communityPostRepository;
    private readonly IPostRepository _postRepository;

    public CommunityService(IMapper mapper, ICommunityRepository communityRepository, ICommunityMemberRepository communityMemberRepository, IPostRepository postRepository, ICommunityPostRepository communityPostRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
        _communityMemberRepository = communityMemberRepository;
        _postRepository = postRepository;
        _communityPostRepository = communityPostRepository;
    }

    public async Task<Community> AddCommunity(Community newCommunity)
    {
        var addedCommunity = await _communityRepository.AddAsync(newCommunity);
        await _communityRepository.SaveAsync();
        return addedCommunity;
    }

    public async Task<CommunityMember> AddCommunityMember(CommunityMember communityMember)
    {
        var addedCommunityMember = await _communityMemberRepository.AddAsync(communityMember);
        await _communityMemberRepository.SaveAsync();
        return addedCommunityMember;
    }

    public async Task<List<Community>> GetCommunities(int limit, int currCursor)
    {
        var communities = await _communityRepository.GetAllAsync();
        return communities!
            .OrderBy(c => c.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<Community?> GetCommunityById(uint communityId)
    {
        return await _communityRepository.GetByIdAsync(communityId);
    }

    public async Task<CommunityMember> GetCommunityOwner(uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        return community!.CommunityMembers
            .First(cm => cm.TypeId == CommunityMemberType.Owner);
    }

    public async Task<Community> DeleteCommunity(uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        if (community != null) _communityRepository.Delete(community);
        await _communityRepository.SaveAsync();
        return community!;
    }

    public async Task<bool> IsUserCommunityMember(uint communityId, uint userId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        var communityMember = community?.CommunityMembers.FirstOrDefault(cm => cm.UserId == userId);
        return communityMember != null;
    }

    public async Task<CommunityPost> AddCommunityPost(uint communityId, Post post, uint proposerId)
    {
        var addedPost = await _postRepository.AddAsync(post);

        await _postRepository.SaveAsync();

        var communityPost = new CommunityPost()
        {
            PostId = addedPost.Id,
            CommunityId = communityId,
            ProposerId = proposerId
        };

        var addedCommunityPost = await _communityPostRepository.AddAsync(communityPost);
        await _communityPostRepository.SaveAsync();

        return addedCommunityPost;
    }

    public async Task<List<CommunityPost>> GetCommunityPosts(uint communityId, int limit, int currCursor)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        return community!.CommunityPosts
            .OrderBy(cp => cp.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<CommunityResponseDto> ChangeCommunity(uint communityId, CommunityPatchRequestDto communityPatchRequestDto)
    {
        var community = await _communityRepository.GetByIdAsync(communityId);
        bool updated = false;
        if (communityPatchRequestDto.CommunityPictureId != null)
        {
            var media = await _communityRepository.GetByIdAsync((uint)communityPatchRequestDto.CommunityPictureId);
            if (media == null)
                throw new Exception($"Media with id equal {communityPatchRequestDto.CommunityPictureId} doesn't exist.");
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
                throw new Exception($"Community name should have at east one character.");
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
                throw new Exception($"Community description should have at east one character.");
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