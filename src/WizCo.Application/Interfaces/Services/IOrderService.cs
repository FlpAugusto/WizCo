using WizCo.Application.Shared.DTOs.Request;
using WizCo.Application.Shared.DTOs.Response;
using WizCo.Application.Shared.Results;
using WizCo.Domain.Filters;

namespace WizCo.Application.Interfaces.Services
{
    public interface IOrderService : IServiceBase
    {
        PagedResult<OrderDto> GetByFilter(OrderFilter filter);

        Task<OrderDto> GetByIdAsync(Guid id);

        Task<OrderDto> CreateAsync(CreateOrderDto data);

        Task CancelAsync(Guid id);
    }
}
