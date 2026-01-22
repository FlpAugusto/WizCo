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

        public Order(string clientName, List<ItemOrder> items) : this()
        {
            ClientName = clientName;
            Items = items;

            CreatedAt = DateTimeOffset.UtcNow;
            CalculateTotalValue();
        }

        public void CalculateTotalValue()
        {
            TotalValue = Items.Sum(i => i.Subtotal);
        }

        public void Pay()
        {
            Status = StatusOrder.Paid;
        }

        public void Cancel()
        {
            if (!CanBeCanceled())
            {
                throw new InvalidOperationException("Pedido pago não pode ser cancelado.");
            }

            Status = StatusOrder.Canceled;
        }

        public bool CanBeCanceled()
        {
            return Status != StatusOrder.Paid;
        }
    }
}
