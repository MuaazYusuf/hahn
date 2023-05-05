using Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace Infrastructure.Data.Repositories
{
    public class AsyncRepository<TEntity, TId> : IBaseAsyncRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        protected readonly EFContext _context;
        protected DbSet<TEntity> entities;
        public AsyncRepository(EFContext context)
        {
            _context = context;
            entities = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TId id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                entities.Remove(user);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}