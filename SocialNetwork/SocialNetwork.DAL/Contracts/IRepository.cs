using System.Linq.Expressions;

namespace SocialNetwork.DAL.Contracts;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereFilter);
    Task<TEntity?> GetEntityByIdAsync(uint id);
    Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> whereFilter);
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveAsync();
}