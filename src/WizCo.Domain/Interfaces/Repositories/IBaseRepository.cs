using System.Linq.Expressions;
using WizCo.Domain.Entities;

namespace WizCo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<bool> CreateAsync(TEntity entity);

        Task<bool> CreateAsync(ICollection<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateAsync(ICollection<TEntity> entities);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

        Task<bool> DeleteAsync(TEntity entity);
    }
}
