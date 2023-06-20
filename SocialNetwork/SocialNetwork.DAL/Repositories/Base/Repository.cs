using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Base;

namespace SocialNetwork.DAL.Repositories.Base;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    protected readonly SocialNetworkContext SocialNetworkContext;

    protected Repository(SocialNetworkContext socialNetworkContext) => SocialNetworkContext = socialNetworkContext;

    public virtual async Task<List<TEntity>> Select(Expression<Func<TEntity, bool>>? whereFilter = null)
    {
        var resultSet = SocialNetworkContext.Set<TEntity>();
        return await (whereFilter == null 
            ? resultSet.ToListAsync() 
            : resultSet.Where(whereFilter).ToListAsync());
    }

    public virtual async Task<TEntity> Add(TEntity entity)
    {
        await SocialNetworkContext.AddAsync(entity);
        await SocialNetworkContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        var entityEntry = SocialNetworkContext.Update(entity);
        await SocialNetworkContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public virtual async Task<TEntity> Delete(TEntity entity)
    {
        var entityEntry = SocialNetworkContext.Remove(entity);
        await SocialNetworkContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
}