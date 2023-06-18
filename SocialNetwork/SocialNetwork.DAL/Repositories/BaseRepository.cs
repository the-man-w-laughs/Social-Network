using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialNetwork.DAL.Context;

namespace SocialNetwork.DAL.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
    protected readonly SocialNetworkContext SocialNetworkContext;

    protected BaseRepository(SocialNetworkContext socialNetworkContext) => SocialNetworkContext = socialNetworkContext;

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
        var existingEntity = await SocialNetworkContext.FindAsync<TEntity>(entity.Id);
        if (existingEntity != null)
        {
            foreach (var property in typeof(TEntity).GetProperties())
            {
                var newValue = property.GetValue(entity);
                var currentValue = property.GetValue(existingEntity);
                if (newValue != null && !newValue.Equals(currentValue))
                {
                    property.SetValue(existingEntity, newValue);
                }
            }
            await SocialNetworkContext.SaveChangesAsync();
        }
        return existingEntity;
    }

    public virtual async Task<TEntity> Delete(TEntity entity)
    {
        var entityEntry = SocialNetworkContext.Remove(entity);
        await SocialNetworkContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
}