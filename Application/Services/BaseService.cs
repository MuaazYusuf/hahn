using Application.Base;
using Domain.Base;

namespace Application.Services
{
    public class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly IBaseAsyncRepository<TEntity, TId> _repository;

        public BaseService(IBaseAsyncRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var createdEntity = await _repository.AddAsync(entity);
            return createdEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(TId id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}