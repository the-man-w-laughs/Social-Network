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

    public async override Task SaveAsync()
    {
        var modifiedEntries = SocialNetworkContext.ChangeTracker.Entries<Comment>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntries)
        {
            var chat = entry.Entity;
            chat.UpdatedAt = DateTime.Now;
        }

        await base.SaveAsync();
    }
}