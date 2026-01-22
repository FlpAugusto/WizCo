namespace WizCo.Application.Shared.DTOs.Request
{
    public class CreateItemDto : DTOBase
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
