using System.Linq.Expressions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Base;

namespace SocialNetwork.DAL.Repositories.Base;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    protected readonly SocialNetworkContext SocialNetworkContext;

    protected Repository(SocialNetworkContext socialNetworkContext) => SocialNetworkContext = socialNetworkContext;

    public virtual async Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>>? whereFilter = null)
    {
        DbSet<TEntity> resultSet = SocialNetworkContext.Set<TEntity>();

        if (whereFilter == null)
        {
            return await resultSet.ToListAsync();
        }
        else
        {
            return await resultSet.Where(whereFilter).ToListAsync();
        }
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await SocialNetworkContext.AddAsync(entity);
        await SocialNetworkContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityQuery = await SocialNetworkContext.FindAsync<TEntity>(id);
        if (entityQuery == null)
        {
            return entityQuery;
        }
        entity.ApplyTo(entityQuery);
        await SocialNetworkContext.SaveChangesAsync();

        return entityQuery;
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var entity = await SocialNetworkContext.FindAsync<TEntity>(id);
        if (entity != null)
        {
            var entityEntry = SocialNetworkContext.Remove(entity);
            await SocialNetworkContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
        else
        {
            return null;
        }
    }
}