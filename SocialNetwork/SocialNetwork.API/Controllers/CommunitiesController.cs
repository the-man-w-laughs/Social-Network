using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]    
public class CommunitiesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMediaService _mediaService;
    private readonly IFileService _fileService;

    public CommunitiesController(IMapper mapper, IMediaService mediaService, IFileService fileService, IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _mediaService = mediaService;
        _fileService = fileService;
    }

    /// <summary>
    /// CreateCommunity
    /// </summary>
    /// <remarks>Create community.</remarks>
    [HttpPost]
    public virtual ActionResult<CommunityResponseDto> PostCommunities([FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var community = new Community { Name = "TestCommunity", Description = "TestCommunityDescription", IsPrivate = false };
        
        return Ok(_mapper.Map<CommunityResponseDto>(community));
    }

    /// <summary>
    /// GetAllCommunities
    /// </summary>
    /// <remarks>Get all communities using pagination.</remarks>    
    [HttpGet]              
    public virtual ActionResult<List<CommunityResponseDto>> GetCommunities(
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var communities = new List<Community>
        {
            new() { Name = "TestCommunity1", Description = "TestCommunityDescription1", IsPrivate = false },
            new() { Name = "TestCommunity2", Description = "TestCommunityDescription2", IsPrivate = true }
        };
        
        return Ok(communities.Select(c => _mapper.Map<CommunityResponseDto>(c)));
    }

    /// <summary>
    /// ChangeCommunityInfo
    /// </summary>
    /// <remarks>Change community info (for community admins, admins).</remarks>        
    [HttpPut]
    [Route("{communityId}")]
    public virtual ActionResult<CommunityResponseDto> PutCommunitiesCommunityId(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var community = new Community { Name = "TestCommunity", Description = "TestCommunityDescription", IsPrivate = false };
        
        return Ok(_mapper.Map<CommunityResponseDto>(community));
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>          
    [HttpDelete]
    [Route("{communityId}")]
    public virtual ActionResult<CommunityResponseDto> DeleteCommunitiesCommunityId([FromRoute, Required] uint communityId)
    {
        var community = new Community { Name = "TestCommunity", Description = "TestCommunityDescription", IsPrivate = false };
        
        return Ok(_mapper.Map<CommunityResponseDto>(community));
    }

    /// <summary>
    /// CreateCommunityPost
    /// </summary>
    /// <remarks>Create community post (depending on isPrivate field it could be available for community members or for everybody).</remarks>         
    [HttpPost]
    [Route("{communityId}/posts")]
    public virtual ActionResult<PostResponseDto> PostCommunitiesCommunityIdPosts(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] PostRequestDto communityRequestDto)
    {
        var post = new Post { Id = 200, Content = "TestCommunityDescription", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    /// <summary>
    /// GetAllCommunityPosts
    /// </summary>
    /// <remarks>Get all community posts using pagination.</remarks>    
    [HttpGet]
    [Route("{communityId}/posts")]
    public virtual ActionResult<List<CommunityPostResponseDto>> GetCommunitiesPosts(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var communityPost = new CommunityPost { Id = 200 };
        
        return Ok(_mapper.Map<PostResponseDto>(communityPost));
    }

    /// <summary>
    /// CreateCommunityMedia
    /// </summary>
    /// <remarks>Create community media.</remarks>    
    [HttpPost]
    [Route("{communityId}/medias")]
    public virtual ActionResult<List<MediaResponseDto>> PostcommunityscommunityIdMedias([FromRoute][Required] uint communityId, [Required] List<IFormFile> files)
    {
        if (files == null)
        {
            return BadRequest();
        }

        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");

        foreach (var file in files)
        {
            string filePath = Path.Combine(directoryPath, file.FileName);
            string modifiedFilePath = _fileService.ModifyFilePath(filePath);
            using (var stream = new FileStream(modifiedFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            _mediaService.AddCommunityMedia(modifiedFilePath, communityId, file.FileName);
        }

        return Ok("Uploaded Successful");
    }

    /// <summary>
    /// GetAllcommunityMedias
    /// </summary>
    /// <remarks>Get all community's posts using pagination. (only for community or admin)</remarks>
    [HttpGet]
    [Route("{communityId}/medias")]
    public virtual ActionResult<List<CommunityMediaOwnerResponseDto>> GetcommunityscommunityIdMedias(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var result = _mediaService.GetCommunityMediaList(communityId, limit, currCursor);
        return Ok(result);
    }
}