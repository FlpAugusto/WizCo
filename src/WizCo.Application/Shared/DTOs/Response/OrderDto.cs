namespace WizCo.Application.Shared.DTOs.Response
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Status { get; set; }

        public decimal TotalValue { get; set; }

        public IEnumerable<ItemOrderDto> Itens { get; set; }
    }
}
