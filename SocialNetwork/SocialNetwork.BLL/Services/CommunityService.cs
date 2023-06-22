using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.Services;

public class CommunityService : ICommunityService
{

    private readonly ICommunityRepository _communityRepository;

    private readonly ICommunityMemberRepository _communityMemberRepository;

    public CommunityService(ICommunityRepository communityRepository, ICommunityMemberRepository communityMemberRepository)
    {
        _communityRepository = communityRepository;
        _communityMemberRepository = communityMemberRepository;
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
}