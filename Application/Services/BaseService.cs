using Application.Base;
using Domain.Base;
using Application.DBExceptions;

namespace Application.Services
{
    public class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly IBaseAsyncRepository<TEntity, TId> _repository;

        public BaseService(IBaseAsyncRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity ?? throw new EntityNotFoundException($"Entity with id {id} was not found");
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

        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await _repository.DeleteAsync(entity);
        }
    }
}