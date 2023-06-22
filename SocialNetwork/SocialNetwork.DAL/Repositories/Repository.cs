using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;

namespace SocialNetwork.DAL.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    protected readonly SocialNetworkContext SocialNetworkContext;

    protected Repository(SocialNetworkContext socialNetworkContext) => SocialNetworkContext = socialNetworkContext;

    public virtual async Task<List<TEntity>> GetAllAsync() =>
        await SocialNetworkContext.Set<TEntity>().ToListAsync();

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where) =>
        SocialNetworkContext.Set<TEntity>().Where(where).ToList();

    public virtual async Task<TEntity?> GetByIdAsync(uint id) =>
        await SocialNetworkContext.Set<TEntity>().FindAsync(id);

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where) =>
        await SocialNetworkContext.Set<TEntity>().FirstOrDefaultAsync(where);

    public async Task<TEntity> AddAsync(TEntity entity) =>
        (await SocialNetworkContext.AddAsync(entity)).Entity;

    public virtual void Update(TEntity entity) =>
        SocialNetworkContext.Entry(entity).State = EntityState.Modified;

    public virtual void Delete(TEntity entity) =>
        SocialNetworkContext.Set<TEntity>().Remove(entity);

    public virtual async Task<TEntity?> DeleteById(uint id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) Delete(entity);
        return entity;
    }

    public virtual async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> where)
    {
        var entities = await GetAllAsync(where);
        SocialNetworkContext.Set<TEntity>().RemoveRange(entities);
    }

    public virtual async Task SaveAsync() =>
        await SocialNetworkContext.SaveChangesAsync();
}