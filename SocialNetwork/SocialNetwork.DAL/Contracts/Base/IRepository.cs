using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace SocialNetwork.DAL.Contracts.Base;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity?>> Select(Expression<Func<TEntity, bool>>? whereFilter = null);
    Task<TEntity> Add(TEntity entity);
    Task<TEntity?> Update(uint id, JsonPatchDocument entity);
    Task<TEntity?> Delete(uint id);
}