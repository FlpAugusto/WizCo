using AspNetCore.IQueryable.Extensions.Sort;
using Microsoft.EntityFrameworkCore;
using WizCo.Domain.Entities;
using WizCo.Domain.Filters;
using WizCo.Domain.Interfaces.Repositories;
using WizCo.Infrastructure.Data;
using WizCo.Infrastructure.Repositories.Queries;

namespace WizCo.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order, WizCoDbContext>, IOrderRepository
    {
        private readonly WizCoDbContext _context;

        public OrderRepository(WizCoDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Order> GetByFilter(OrderFilter filter)
        {
            var query = _context.Orders
                 .AsNoTracking()
                 .Include(o => o.Items)
                 .AsQueryable();

            query = new OrderQueryBuilder(query)
                 .AddStatus(filter.Status)
                 .Build();

            return query.Sort(filter.Sort);
        }

        public async Task<Order> GetByIdWithItemsAsync(Guid id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetByIdWithItemsTrackingAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
