using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.Services;

public class CommunityService : ICommunityService
{

    private readonly ICommunityRepository _communityRepository;

    private readonly ICommunityMemberRepository _communityMemberRepository;

    private readonly ICommunityPostRepository _communityPostRepository;

    private readonly IPostRepository _postRepository;

    public CommunityService(ICommunityRepository communityRepository, ICommunityMemberRepository communityMemberRepository, IPostRepository postRepository, ICommunityPostRepository communityPostRepository)
    {
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
}