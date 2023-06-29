using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Services;

public class AdminService : IAdminService
{
    private readonly IMapper _mapper;

    public AdminService(IMapper mapper)
    {
        _mapper = mapper;
    }
  
}