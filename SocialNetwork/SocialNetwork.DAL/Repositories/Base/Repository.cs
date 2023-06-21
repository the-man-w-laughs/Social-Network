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
        var resultSet = SocialNetworkContext.Set<TEntity>();

        if (whereFilter == null)
        {
            return await resultSet.ToListAsync();
        }
        else
        {
            return await resultSet.Where(whereFilter).ToListAsync();
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await SocialNetworkContext.AddAsync(entity);
        return entity;
    }

    public virtual void Update(TEntity entity)
    {
        SocialNetworkContext.Entry(entity).State = EntityState.Modified;        
    }

    public virtual void Delete(TEntity entity)
    {
        SocialNetworkContext.Set<TEntity>().Remove(entity);
    }

    public virtual async Task SaveAsync()
    {
        await SocialNetworkContext.SaveChangesAsync();
    }
}