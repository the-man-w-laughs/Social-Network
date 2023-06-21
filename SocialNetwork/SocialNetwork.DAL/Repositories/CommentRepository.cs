using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}