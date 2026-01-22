namespace WizCo.Domain.Entities
{
    public class ItemOrder : EntityBase
    {
        public string ProductName { get; private set; }

        public int Amount { get; private set; }

        public decimal UnitPrice { get; private set; }

        //Relacionamentos
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
    }
}
