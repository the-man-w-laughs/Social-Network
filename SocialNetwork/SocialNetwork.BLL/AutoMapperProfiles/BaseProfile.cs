using AutoMapper;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.AutoMapper;

public class BaseProfile : Profile
{
    public BaseProfile()
    {        
    }
}