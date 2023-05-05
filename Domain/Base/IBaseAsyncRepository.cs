namespace Domain.Base
{
    public interface IBaseAsyncRepository<TEntity, TId> where TEntity  : BaseEntity<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);
        
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}