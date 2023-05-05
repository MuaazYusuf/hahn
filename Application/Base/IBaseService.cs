using Domain.Base;

namespace Application.Base
{
    public interface IBaseService<TEntity, TId> where TEntity  : BaseEntity<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);
        
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TId id);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}