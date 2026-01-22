using WizCo.Domain.Enums;

namespace WizCo.Domain.Entities
{
    public class Order : EntityBase
    {
        public string ClientName { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public StatusOrder Status { get; private set; }

        public decimal TotalValue { get; private set; }

        //Relacionamentos
        public ICollection<ItemOrder> Items { get; private set; }

        public Order()
        {
            Status = StatusOrder.New;
            Items = [];
        }

        public Order(string clientName, IEnumerable<ItemOrder> items) : this()
        {
            ClientName = clientName;
            Items = items.ToList();

            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
