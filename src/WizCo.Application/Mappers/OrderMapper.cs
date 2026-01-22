using WizCo.Application.Shared.DTOs.Request;
using WizCo.Application.Shared.DTOs.Response;
using WizCo.Domain.Entities;

namespace WizCo.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order ToEntity(CreateOrderDto dto)
        {
            var items = dto.Itens
                .Select(i => new ItemOrder(productName: i.ProductName,
                                           amount: i.Quantity,
                                           unitPrice: i.UnitPrice))
                .ToList();

            return new Order(dto.ClientName, items);
        }

        public static OrderDto ToModel(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                ClientName = order.ClientName,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(),
                TotalValue = order.TotalValue,
                Itens = order.Items.Select(i => new ItemOrderDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Amount = i.Amount,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public static IEnumerable<OrderDto> ToModel(IEnumerable<Order> orders)
        {
            return orders.Select(ToModel);
        }
    }
}
