namespace WizCo.Application.Shared.DTOs.Response
{
    public class ItemOrderDto
    {
        public Guid Id { get; set; }

        public string ProductName { get; private set; }

        public int Amount { get; private set; }

        public decimal UnitPrice { get; private set; }
    }
}
