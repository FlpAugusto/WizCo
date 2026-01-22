using WizCo.Domain.Shared;

namespace WizCo.Domain.Filters
{
    public class OrderFilter : QueryBase
    {
        public string? Status { get; set; }

        protected override string DefaultSort => nameof(Entities.Order.ClientName);
    }
}
