namespace WizCo.Application.Shared.DTOs.Response
{
    public class ItemOrderDto
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
