using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;


public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}

    public async override Task SaveAsync()
    {
        var modifiedEntries = SocialNetworkContext.ChangeTracker.Entries<Message>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntries)
        {
            var chat = entry.Entity;
            chat.UpdatedAt = DateTime.Now;
        }

        await base.SaveAsync();
    }
}