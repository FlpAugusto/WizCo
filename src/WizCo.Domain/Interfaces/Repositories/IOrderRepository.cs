using WizCo.Domain.Entities;
using WizCo.Domain.Filters;

namespace WizCo.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetByFilter(OrderFilter filter);

        Task<Order> GetByIdWithItemsAsync(Guid id);

        Task<Order> GetByIdWithItemsTrackingAsync(Guid id);
    }
}
