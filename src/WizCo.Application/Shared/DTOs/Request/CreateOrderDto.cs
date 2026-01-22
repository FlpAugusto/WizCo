namespace WizCo.Application.Shared.DTOs.Request
{
    public class CreateOrderDto : DTOBase
    {
        public string ClientName { get; set; }

        public IEnumerable<CreateItemDto> Itens { get; set; }
    }
}
