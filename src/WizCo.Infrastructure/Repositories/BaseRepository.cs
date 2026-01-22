using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WizCo.Domain.Entities;
using WizCo.Domain.Interfaces.Repositories;

namespace WizCo.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
    where TEntity : EntityBase
    where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> CreateAsync(ICollection<TEntity> entities)
        {
            _context.AddRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(ICollection<TEntity> entities)
        {
            _context.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            entity.Delete();
            return await UpdateAsync(entity);
        }
    }
}
