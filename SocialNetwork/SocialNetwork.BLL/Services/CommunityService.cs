using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.Services;

public class CommunityService : ICommunityService
{
    private readonly IMapper _mapper;
    private readonly IMediaRepository _mediaRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly ICommunityMemberRepository _communityMemberRepository;    
    private readonly IPostRepository _postRepository;

    public CommunityService(
        IMapper mapper,
        IMediaRepository mediaRepository,
        ICommunityRepository communityRepository,
        ICommunityMemberRepository communityMemberRepository,
        IPostRepository postRepository)
    {
        _mapper = mapper;
        _mediaRepository = mediaRepository;
        _communityRepository = communityRepository;
        _communityMemberRepository = communityMemberRepository;
        _postRepository = postRepository;        
    }

    public async Task<CommunityResponseDto> AddCommunity(CommunityPostDto communityRequestDto)
    {       
        var community = _mapper.Map<Community>(communityRequestDto);
        community.CreatedAt = DateTime.Now;
        await _communityRepository.AddAsync(community);
        await _communityRepository.SaveAsync();
        return _mapper.Map<CommunityResponseDto>(community);
    }

    public async Task<CommunityResponseDto> GetCommunity(uint userId, uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ??
            throw new NotFoundException("No community with this Id.");
        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null && community.IsPrivate)
            throw new OwnershipException("Only community members can get private community posts.");
        var result = _mapper.Map<CommunityResponseDto>(community);
        result.UserCount = (await _communityMemberRepository.GetAllAsync(m => m.CommunityId == communityId)).Count();
        return result;
    }

    public async Task<CommunityMemberResponseDto> AddCommunityOwner(uint communityId, uint userIdToAdd)
    {
        var newCommunityMember = new CommunityMember()
        {
            CommunityId = communityId,
            CreatedAt = DateTime.Now,
            TypeId = CommunityMemberType.Owner,
            UserId = userIdToAdd
        };
        var addedCommunityMember = await _communityMemberRepository.AddAsync(newCommunityMember);
        await _communityMemberRepository.SaveAsync();
        return _mapper.Map<CommunityMemberResponseDto>(addedCommunityMember);
    }

    public async Task<CommunityMemberResponseDto> AddCommunityMember(uint userId, uint communityId, uint userIdToAdd)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ??
            throw new NotFoundException("No community with this Id.");

        if (await GetCommunityMember(communityId, userIdToAdd) != null)
            throw new DuplicateEntryException("User is already a community member.");

        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null && community.IsPrivate)
            throw new OwnershipException("Only community members can add new members to private communities.");

        var newCommunityMember = new CommunityMember()
        {
            CommunityId = communityId,
            CreatedAt = DateTime.Now,
            TypeId = CommunityMemberType.Member,
            UserId = userIdToAdd
        };
        var addedCommunityMember = await _communityMemberRepository.AddAsync(newCommunityMember);
        await _communityMemberRepository.SaveAsync();
        return _mapper.Map<CommunityMemberResponseDto>(addedCommunityMember);
    }

    public async Task<CommunityMemberResponseDto> ChangeCommunityMember(uint userId, uint communityId, uint userIdToChange, CommunityMemberPutDto communityMemberRequestDto)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ??
            throw new NotFoundException("No community with this Id.");
        var communityMemberToChange = await GetCommunityMember(communityId, userIdToChange);
        if (communityMemberToChange == null)
            throw new NotFoundException("User is not a community member.");

        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null)
            throw new OwnershipException("Only community members can change members in communities.");        

        if (communityMember.TypeId == CommunityMemberType.Owner)
        {
            if (userIdToChange == userId)
            {
                throw new OwnershipException("Owner cant change himself.");
            }            
        }

        if (communityMember.TypeId == CommunityMemberType.Admin)
        {
            if (userIdToChange != userId && communityMemberRequestDto.TypeId != CommunityMemberType.Member)
            {
                throw new OwnershipException("Admin can only change himself to user.");
            }
        }

        if (communityMember.TypeId == CommunityMemberType.Member)
        {
            throw new OwnershipException("Member can't change anything.");
        }

        communityMemberToChange.UpdatedAt = DateTime.Now;
        communityMemberToChange.TypeId = communityMemberRequestDto.TypeId;
        _communityMemberRepository.Update(communityMemberToChange);
        await _communityMemberRepository.SaveAsync();
        return _mapper.Map<CommunityMemberResponseDto>(communityMemberToChange);
    }

    public async Task<CommunityMemberResponseDto> DeleteCommunityMember(uint userId, uint communityId, uint userIdToDelete)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ??
            throw new NotFoundException("No community with this Id.");
        var communityMemberToDelete = await GetCommunityMember(communityId, userIdToDelete);
        if (communityMemberToDelete == null)
            throw new NotFoundException("User is not a community member.");

        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null)
            throw new OwnershipException("Only community members can delete members from communities.");

        if (userIdToDelete != userId)
        {
            if (communityMember.TypeId == CommunityMemberType.Member)
            {
                throw new OwnershipException("Community members can't delete community members.");
            }
            else if (communityMember.TypeId == CommunityMemberType.Admin && communityMemberToDelete.TypeId == CommunityMemberType.Admin)
            {
                throw new OwnershipException("Community admin can't delete community admin.");
            }
            else if (communityMember.TypeId == CommunityMemberType.Admin && communityMemberToDelete.TypeId == CommunityMemberType.Owner)
            {
                throw new OwnershipException("Community admin can't delete community owner.");
            }
        }
        else
        {
            if (communityMemberToDelete.TypeId == CommunityMemberType.Owner)
            {
                throw new OwnershipException("Owner can't delete himself.");
            }
        }     

        _communityMemberRepository.Delete(communityMemberToDelete);
        await _communityMemberRepository.SaveAsync();
        return _mapper.Map<CommunityMemberResponseDto>(communityMemberToDelete);
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
    private async Task<CommunityMember?> GetCommunityMember(uint communityId, uint userId)
    {
        return await _communityMemberRepository.GetAsync(m => m.UserId == userId && m.CommunityId == communityId);
    }

    public async Task<CommunityResponseDto> DeleteCommunity(uint communityId)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ?? throw new NotFoundException("No community with this Id.");
        _communityRepository.Delete(community);
        await _communityRepository.SaveAsync();
        return _mapper.Map<CommunityResponseDto>(community);
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

    public async Task<List<PostResponseDto>> GetCommunityPosts(uint userId, uint communityId, int limit, int currCursor)
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
        return _mapper.Map<List<PostResponseDto>>(pagenatedPosts);       

    }
    public async Task<CommunityResponseDto> ChangeCommunity(uint userId, uint communityId, CommunityPatchDto communityPatchRequestDto)
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
        if (communityPatchRequestDto.Name != null && community.Name != communityPatchRequestDto.Name)
        {                            
            community.Name = communityPatchRequestDto.Name;
            updated = true;                           
        }
        if (communityPatchRequestDto.Description != null && community.Description != communityPatchRequestDto.Description)
        {

            community.Description = communityPatchRequestDto.Description;
            updated = true;                            
        }
        if (communityPatchRequestDto.IsPrivate != null && community.IsPrivate != communityPatchRequestDto.IsPrivate)
        {            
            community.IsPrivate = (bool)communityPatchRequestDto.IsPrivate;
            updated = true;            
        }
        if (updated)
        {
            community!.UpdatedAt = DateTime.Now;
            _communityRepository.Update(community!);
            await _communityRepository.SaveAsync();
        }
        return _mapper.Map<CommunityResponseDto>(community);
    }

    public async Task<List<CommunityMemberResponseDto>> GetCommunityMembers(uint userId, uint communityId, uint? communityMemberTypeId, int limit, int currCursor)
    {
        var community = await _communityRepository.GetByIdAsync(communityId) ??
            throw new NotFoundException("No community with this Id.");
        var communityMember = await GetCommunityMember(communityId, userId);
        if (communityMember == null && community.IsPrivate)
            throw new OwnershipException("Only community members can get info about private communities.");

        List<CommunityMember> members;
        if (communityMemberTypeId != null)
        {
            members = await _communityMemberRepository.GetAllAsync(m => m.TypeId == (CommunityMemberType)communityMemberTypeId);
        }
        else
        {
            members = await _communityMemberRepository.GetAllAsync();
        }
        var paginatedMembers = members.OrderBy(c => c.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
        return _mapper.Map<List<CommunityMemberResponseDto>>(paginatedMembers);
    }
}