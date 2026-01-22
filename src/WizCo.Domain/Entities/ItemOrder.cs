namespace WizCo.Domain.Entities
{
    public class ItemOrder : EntityBase
    {
        public string ProductName { get; private set; }

        public int Amount { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Subtotal => Amount * UnitPrice;

        //Relacionamentos
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public ItemOrder()
        {
        }

        public ItemOrder(string productName, int amount, decimal unitPrice)
        {
            ProductName = productName;
            Amount = amount;
            UnitPrice = unitPrice;
        }
    }
}
