using AutoMapper;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
    }
}