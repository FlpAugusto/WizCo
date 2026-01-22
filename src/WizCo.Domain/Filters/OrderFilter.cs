using WizCo.Application.Shared.Queries;

namespace WizCo.Domain.Filters
{
    public class OrderFilter : QueryBase
    {
        public string StatusOrder { get; set; }

        protected override string DefaultSort => nameof(Entities.Order.CreatedAt);
    }
}
