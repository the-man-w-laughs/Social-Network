using System.Linq.Expressions;

namespace SocialNetwork.DAL.Contracts.Base;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>>? whereFilter = null);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}