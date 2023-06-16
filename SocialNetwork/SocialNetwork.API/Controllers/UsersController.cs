using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <remarks>Returns all users using pagination.</remarks>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>        
        [HttpGet]        
        public virtual IActionResult GetUsers([FromQuery][Required()]decimal? limit, [FromQuery]decimal? currCursor)
        {
            return Ok("GetAllUsers");
        }

        /// <summary>
        /// GetAllUserChats
        /// </summary>
        /// <remarks>Get all users chats using pagination (for account owner).</remarks>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{userId}/chats")]        
        public virtual IActionResult GetUsersUserIdChats([FromQuery][Required()]decimal? limit, [FromQuery]decimal? nextCursor)
        {
            return Ok("GetAllUserChats");
        }

        /// <summary>
        /// GetAllUserCommunities
        /// </summary>
        /// <remarks>Get user&#x27;s communities using pagination.</remarks>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{userId}/communities")]
        public virtual IActionResult GetUsersUserIdCommunities([FromRoute][Required] string userId, [FromQuery][Required()] decimal? limit, [FromQuery] decimal? nextCursor)
        {
            return Ok("GetAllUserCommunities");
        }

        /// <summary>
        /// GetAllUserFriends
        /// </summary>
        /// <remarks>Get all user&#x27;s friends using pagination.</remarks>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{userId}/friends")]  
        public virtual IActionResult GetUsersUserIdFriends([FromRoute][Required]string userId, [FromQuery][Required()]decimal? limit, [FromQuery]decimal? nextCursor)
        {
            return Ok("GetAllUserFriends");
        }

        /// <summary>
        /// GetAllUserPosts
        /// </summary>
        /// <remarks>Get all user&#x27;s posts using pagination.</remarks>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>
        [HttpGet]
        [Route("{userId}/posts")]
        public virtual IActionResult GetUsersUserIdPosts([FromRoute][Required]string userId, [FromQuery]decimal? limit, [FromQuery]decimal? currCursor)
        {
            return Ok("GetAllUserPosts");
        }

        /// <summary>
        /// GetUserProfileInfo
        /// </summary>
        /// <remarks>Get user&#x27;s info.</remarks>
        /// <param name="userId"></param>        
        [HttpGet]
        [Route("{userId}/profile")]
        public virtual IActionResult GetUsersUserIdProfile([FromRoute][Required]string userId)
        {
            return Ok("GetUserProfileInfo");
        }

        /// <summary>
        /// ChangeUserActivityFields
        /// </summary>
        /// <remarks>Makes user&#x27;s account deactivated (for account owner or admin).</remarks>
        /// <param name="userId"></param>        
        [HttpPatch]
        [Route("{userId}")]
        public virtual IActionResult PatchUsersUserId([FromRoute][Required]string userId)
        {
            return Ok("ChangeUserActivityFields");
        }

        /// <summary>
        /// ChangeUserAuthFields
        /// </summary>
        /// <remarks>Change authentification fields.</remarks>
        /// <param name="userId"></param>
        /// <param name="body">Conrains fields to change.</param>        
        [HttpPatch]
        [Route("{userId}/auth")]
        public virtual IActionResult PatchUsersUserIdAuth([FromRoute][Required]string userId)
        {
            return Ok("ChangeUserAuthFields");
        }

        /// <summary>
        /// ChangeUserProfileInfo
        /// </summary>
        /// <remarks>Change personal info (status, sex).</remarks>
        /// <param name="userId"></param>
        /// <param name="body">Contains fields to change.</param>        
        [HttpPatch]
        [Route("{userId}/profile")]
        public virtual IActionResult PatchUsersUserIdProfile([FromRoute][Required]string userId)
        {
            return Ok("ChangeUserProfileInfo");
        }

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <remarks>Creates new user using login and password.</remarks>
        /// <param name="body"></param>
        [HttpPost]        
        public virtual IActionResult PostUsers()
        {
            return Ok("CreateUser");
        }

        /// <summary>
        /// CreateUserPost
        /// </summary>
        /// <remarks>Create user&#x27;s post.</remarks>
        /// <param name="userId"></param>
        [HttpPost]
        [Route("{userId}/posts")]
        public virtual IActionResult PostUsersUserIdPosts([FromRoute][Required]string userId)
        {
            return Ok("CreateUserPost");
        }
    }
}
