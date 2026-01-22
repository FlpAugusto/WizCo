using WizCo.Application.Shared.DTOs.Request;
using WizCo.Application.Shared.DTOs.Response;
using WizCo.Application.Shared.Results;
using WizCo.Domain.Filters;
using WizCo.Infrastructure.Services.Interfaces;

namespace WizCo.Domain.Interfaces.Services
{
    public interface IOrderService : IServiceBase
    {
        Task<PagedResult<OrderDto>> GetByFilterAsync(OrderFilter filter);

        Task<OrderDto> GetByIdAsync(Guid id);

        Task<OrderDto> CreateAsync(CreateOrderDto data);

        Task CancelAsync(Guid id);
    }
}
