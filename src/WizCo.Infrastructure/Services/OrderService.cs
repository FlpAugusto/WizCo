using WizCo.Application.Interfaces.Services;
using WizCo.Application.Mappers;
using WizCo.Application.Services;
using WizCo.Application.Shared.DTOs.Request;
using WizCo.Application.Shared.DTOs.Response;
using WizCo.Application.Shared.Factories;
using WizCo.Application.Shared.Results;
using WizCo.Application.Validators;
using WizCo.Domain.Filters;
using WizCo.Domain.Interfaces.Repositories;

namespace WizCo.Infrastructure.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IServiceContext serviceContext,
                            IOrderRepository repository) : base(serviceContext)
        {
            _repository = repository;
        }

        public PagedResult<OrderDto> GetByFilter(OrderFilter filter)
        {
            var baseQuery = _repository.GetByFilter(filter);

            var response = baseQuery.Select(o => new OrderDto
            {
                Id = o.Id,
                ClientName = o.ClientName,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                TotalValue = o.TotalValue,

                Itens = o.Items.Select(i => new ItemOrderDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Amount = i.Amount,
                    UnitPrice = i.UnitPrice
                })
            });

            return PagedResultFactory<OrderDto>.Create(filter, response);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdWithItemsAsync(id);
            if (order is null)
            {
                return null;
            }

            return OrderMapper.ToModel(order);
        }

        public async Task<OrderDto?> CreateAsync(CreateOrderDto data)
        {
            if (!await ValidateCreateDataAsync(data))
            {
                return null;
            }

            var order = OrderMapper.ToEntity(data);
            await _repository.CreateAsync(order);

            return OrderMapper.ToModel(order);
        }

        public async Task CancelAsync(Guid id)
        {
            var order = await _repository.GetByIdWithItemsTrackingAsync(id);
            if (order is null)
            {
                AddNotification("Pedido não encontrado.");
                return;
            }

            if (!order.CanBeCanceled())
            {
                AddNotification("Pedido pago não pode ser cancelado.");
                return;
            }

            try
            {
                order.Cancel();
                await _repository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                AddNotification(ex.Message);
            }
        }

        #region [ Private Methods ]

        private async Task<bool> ValidateCreateDataAsync(CreateOrderDto data)
        {
            await data.ValidateAsync(new CreateOrderDtoValidator());
            if (data.IsValid)
            {
                return true;
            }

            AddNotifications(data.ValidationResult);
            return false;
        }

        #endregion
    }
}
