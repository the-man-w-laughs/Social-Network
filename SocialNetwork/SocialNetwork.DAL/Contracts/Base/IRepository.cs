using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace SocialNetwork.DAL.Contracts.Base;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>>? whereFilter = null);

    Task<TEntity> AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);    

    Task SaveAsync();
}