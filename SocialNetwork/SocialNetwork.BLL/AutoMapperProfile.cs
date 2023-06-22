using AutoMapper;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Chats
        CreateMap<ChatRequestDto, Chat>();
        CreateMap<ChatMemberRequestDto, Chat>();

        CreateMap<Chat, ChatResponseDto>();
        CreateMap<ChatMember, ChatMemberResponseDto>();

        // Comments
        CreateMap<CommentLike, CommentLikeResponseDto>();
        CreateMap<Comment, CommentResponseDto>();

        // Communities
        CreateMap<CommunityPost, CommunityPostResponseDto>();
        CreateMap<Community, CommunityResponseDto>();

        // Messages
        CreateMap<MessageLike, MessageLikeResponseDto>();
        CreateMap<Message, MessageResponseDto>();
        
        // Posts
        CreateMap<PostLike, PostLikeResponseDto>();
        CreateMap<Post, PostResponseDto>();
        CreateMap<UserProfilePost, UserProfilePostResponseDto>();

        // Users
        CreateMap<UserChangeLoginRequestDto, User>();

        CreateMap<User, UserActivityResponseDto>();
        CreateMap<User, UserEmailResponseDto>();
        CreateMap<User, UserLoginResponseDto>();
        CreateMap<User, UserPasswordResponseDto>();
        CreateMap<UserProfile, UserProfileResponseDto>();        
        CreateMap<User, UserResponseDto>();
    }
}