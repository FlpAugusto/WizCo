using WizCo.Domain.Entities;
using WizCo.Domain.Enums;
using WizCo.Infrastructure.Data;

namespace WizCo.Infrastructure.Repositories.Queries
{
    public class OrderQueryBuilder : BaseQueryBuilder<Order>
    {
        public OrderQueryBuilder(IQueryable<Order> initialQuery) : base(initialQuery) { }

        public OrderQueryBuilder AddStatus(string status)
        {
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<StatusOrder>(status, true, out var parsedStatus))
                {
                    query = query.Where(x => x.Status == parsedStatus);
                }
            }

            return this;
        }
    }
}
