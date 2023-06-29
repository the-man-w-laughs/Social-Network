using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MediasController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMediaService _mediaService;
    private readonly IFileService _fileService;

    public MediasController(IMapper mapper, IMediaService mediaService, IFileService fileService,
        IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _mediaService = mediaService;
        _fileService = fileService;
    }
    /// <summary>Create User Media</summary>
    /// <remarks>Create user media.</remarks>    
    [Authorize(Roles = "User")]
    [HttpPost]
    public virtual async Task<ActionResult<List<MediaResponseDto>>> PostUsersUserIdMedias([Required] List<IFormFile> files)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        if (files.Count == 0)
            BadRequest("File list can't be empty");

        var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var medias = new List<MediaResponseDto>(files.Count);

        foreach (var file in files)
        {
            var filePath = Path.Combine(directoryPath, file.FileName);
            var modifiedFilePath = _fileService.ModifyFilePath(filePath);
            await using (var stream = new FileStream(modifiedFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var addedMedia = await _mediaService.AddUserMedia(modifiedFilePath, userId, file.FileName);
            medias.Add(addedMedia);
        }

        return Ok(medias);
    }

    /// <summary>
    /// GetMediaInfo
    /// </summary>
    /// <remarks>Get media info.</remarks>          
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the media file.</response>
    /// <response code="404">If the media file is not found.</response>
    [HttpGet]
    [Route("{mediaId}/info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<MediaResponseDto> GetMediasMediaIdInfo([FromRoute][Required] uint mediaId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var media = await _mediaService.GetMedia(mediaId);
        return media;
    }

    /// <summary>
    /// DownloadMedia
    /// </summary>
    /// <remarks>Download media.</remarks>          
    /// <param name="mediaId">The ID of the media.</param>    
    /// <response code="200">Returns the media file.</response>
    /// <response code="404">If the media file is not found.</response>
    [HttpGet]
    [Route("{mediaId}/download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediasMediaIdDownload([FromRoute][Required] uint mediaId)
    {
        var localMedia = await _mediaService.GetLocalMedia(mediaId);

        if (System.IO.File.Exists(localMedia.FilePath))
        {
            var fileBytes = System.IO.File.ReadAllBytes(localMedia.FilePath);
            var contentType = _fileService.GetFileType(localMedia.FileName);

            var fileContentResult = new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = localMedia.FileName
            };
            return fileContentResult;
        }

        throw new Exception("Not file in local storage.");
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
    [Authorize(Roles = "User")]
    [Route("{mediaId}")]
    [ProducesResponseType(typeof(MediaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<MediaResponseDto>> DeleteMediasMediaId([FromRoute] [Required] uint mediaId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedMedia = await _mediaService.DeleteMedia(userId, mediaId);
        return deletedMedia;
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
    public virtual async Task<ActionResult<MediaLikeResponseDto>> PostMediasMediaIdLikes(
        [FromRoute] [Required] uint mediaId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();        
        return Ok(await _mediaService.LikeMedia(userId, mediaId));
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
    [Authorize(Roles = "User")]
    [Route("{mediaId}/likes")]
    [ProducesResponseType(typeof(List<MediaLikeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<List<MediaLikeResponseDto>>> GetMediasMediaId(
        [FromRoute] [Required] uint mediaId, [FromQuery] [Required] int limit, [FromQuery] int currCursor)
    {        
        return Ok(await _mediaService.GetMediaLikes(mediaId, limit, currCursor));
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
    public virtual async Task<ActionResult<MediaLikeResponseDto>> DeleteMediasMediaIdLikes(
        [FromRoute] [Required] uint mediaId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        return Ok(await _mediaService.UnLikeMedia(userId, mediaId));
    }
}