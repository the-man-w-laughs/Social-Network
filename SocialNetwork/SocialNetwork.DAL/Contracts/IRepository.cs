using System.Linq.Expressions;

namespace SocialNetwork.DAL.Contracts;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity?> GetByIdAsync(uint id);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> where);
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> DeleteById(uint id);
    Task DeleteRangeAsync(Expression<Func<TEntity, bool>> where);
    Task SaveAsync();
}