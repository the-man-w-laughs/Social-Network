using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public async override Task SaveAsync()
    {
        var modifiedEntries = SocialNetworkContext.ChangeTracker.Entries<User>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in modifiedEntries)
        {
            var user = entry.Entity;
            var original = entry.OriginalValues;

            if (entry.Property(u => u.PasswordHash).IsModified)
            {
                user.PasswordUpdatedAt = DateTime.Now;
            }

            if (entry.Property(u => u.Login).IsModified)
            {
                user.LoginUpdatedAt = DateTime.Now;
            }

            if (entry.Property(u => u.Email).IsModified)
            {
                user.EmailUpdatedAt = DateTime.Now;
            }
        }
        await base.SaveAsync();
    }

    public List<User> GetAllUsersPaginated(int limit, int? currCursor)
    {
        return SocialNetworkContext.Users
            .OrderBy(u => u.Id)
            .Where(p => p.Id > currCursor)
            .Take(limit)
            .ToList();
    }
}