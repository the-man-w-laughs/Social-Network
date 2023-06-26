using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MediasController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMediaService _mediaService;
    private readonly IFileService _fileService;

    public MediasController(IMapper mapper, IMediaService mediaService, IFileService fileService, IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _mediaService = mediaService;
        _fileService = fileService;
    }

    /// <summary>
    /// GetMedia
    /// </summary>
    /// <remarks>Download media (complicated logic).</remarks>          
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the media file.</response>
    /// <response code="404">If the media file is not found.</response>
    [HttpGet]
    [Route("{mediaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediasMediaId([FromRoute][Required] uint mediaId)
    {
        try
        {
            var localMedia = await _mediaService.GetLocalMedia(mediaId);

            if (System.IO.File.Exists(localMedia.FilePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(localMedia.FilePath);
                string contentType = _fileService.GetFileType(localMedia.FileName);

                var fileContentResult = new FileContentResult(fileBytes, contentType)
                {
                    FileDownloadName = localMedia.FileName
                };

                return fileContentResult;
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// DeleteMedia
    /// </summary>    
    /// <remarks>Delete media (for media owners).</remarks>                   
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the deleted media.</response>
    /// <response code="404">If the media is not found.</response>
    /// <response code="500">If an error occurs during the deletion process.</response>
    [HttpDelete]
    [Authorize(Roles = "Admin, User")]
    [Route("{mediaId}")]
    [ProducesResponseType(typeof(MediaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async virtual Task<ActionResult<MediaResponseDto>> DeleteMediasMediaId([FromRoute][Required] uint mediaId)
    {
        var isUserAuthenticated = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claimUserId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        try
        {
            var deletedMedia = await _mediaService.DeleteMedia(claimUserId, mediaId);
            if (System.IO.File.Exists(deletedMedia.FilePath))
            {
                System.IO.File.Delete(deletedMedia.FilePath);
                return Ok(_mapper.Map<MediaLikeResponseDto>(deletedMedia));
            }
            else
            {
                return NotFound("No media with this id.");
            }
        }
        catch (OwnershipException ex)
        {
            return Forbid(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// LikeMedia
    /// </summary>
    /// <remarks>Like media.</remarks>        
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the liked media.</response>
    /// <response code="404">If the media is not found.</response>
    /// <response code="409">If the user has already liked the media.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("{mediaId}/likes")]
    [ProducesResponseType(typeof(MediaLikeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public virtual async Task<ActionResult<MediaLikeResponseDto>> PostMediasMediaIdLikes([FromRoute][Required] uint mediaId)
    {
        var isUserAuthenticated = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claimUserId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);
        try
        {
            await _mediaService.GetLocalMedia(mediaId);            
            return Ok(await _mediaService.LikeMedia(claimUserId, mediaId));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }           
        catch (DuplicateEntryException ex)
        {
            return Conflict(ex.Message);
        }
    }


    /// <summary>
    /// GetAllMediaLikes    
    /// </summary>
    /// <remarks>Get all media likes using pagination (for chat members).</remarks>    
    /// <param name="mediaId">The ID of the media.</param>
    /// <param name="limit">The maximum number of likes to retrieve.</param>
    /// <param name="currCursor">The cursor indicating the current position in the likes list.</param>    
    /// <response code="200">Returns the list of media likes.</response>
    /// <response code="404">If the media is not found.</response>
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{mediaId}/likes")]
    [ProducesResponseType(typeof(List<MediaLikeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async virtual Task<ActionResult<List<MediaLikeResponseDto>>> GetMediasMediaId([FromRoute][Required] uint mediaId, [FromQuery][Required] int limit, [FromQuery] int currCursor)
    {
        try
        {
            await _mediaService.GetLocalMedia(mediaId);
            return Ok(await _mediaService.GetMediaLikes(mediaId, limit, currCursor));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// UnlikeMedia
    /// </summary>
    /// <remarks>Unlike media (for like owner).</remarks>    
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the unliked media like.</response>
    /// <response code="404">If the media is not found or the user has not liked the media.</response>
    [HttpDelete]
    [Authorize(Roles = "User")]
    [Route("{mediaId}/likes")]
    [ProducesResponseType(typeof(MediaLikeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async virtual Task<ActionResult<MediaLikeResponseDto>> DeleteMediasMediaIdLikes([FromRoute][Required] uint mediaId)
    {
        var isUserAuthenticated = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claimUserId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        try
        {
            await _mediaService.GetLocalMedia(mediaId);
            return Ok(await _mediaService.UnLikeMedia(claimUserId, mediaId));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}