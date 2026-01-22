using Microsoft.AspNetCore.Mvc;
using System.Net;
using WizCo.Application.Interfaces.Services;
using WizCo.Application.Shared.DTOs.Request;
using WizCo.Application.Shared.DTOs.Response;
using WizCo.Application.Shared.Results;
using WizCo.Domain.Filters;

namespace WizCo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IServiceContext serviceContext,
                                IOrderService service) : base(serviceContext)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de Pedidos paginada e filtrada.
        /// </summary>
        /// <param name="filter">Filtro e paginação</param>
        /// <returns>Lista paginada das Pedidos filtrados</returns>
        /// <response code="200">Sucesso na requisição</response>
        /// <response code="400">Os parâmetros não foram passados corretamente ou ocorreu algum erro inesperado durante a execução do método</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiBadRequestResult), StatusCodes.Status400BadRequest)]
        public IActionResult GetByFilter([FromQuery] OrderFilter filter)
        {
            var result = _service.GetByFilter(filter);
            return ServiceResponse(result, HttpStatusCode.OK);
        }

        /// <summary>
        /// Retorna uma Pedido através do Id.
        /// </summary>
        /// <param name="id">Id do Pedido</param>
        /// <returns>Dados do Pedido consultado</returns>
        /// <response code="200">Sucesso na requisição</response>
        /// <response code="204">Sucesso na requisição, porém não existem dados</response>
        /// <response code="400">Os parâmetros não foram passados corretamente ou ocorreu algum erro inesperado durante a execução do método</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiBadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return result is null
                ? ServiceResponse(HttpStatusCode.NoContent)
                : ServiceResponse(result, HttpStatusCode.OK);
        }

        /// <summary>
        /// Criar um Pedido.
        /// </summary>
        /// <returns>Dados do Pedido criado</returns>
        /// <response code="201">Sucesso na requisição</response>
        /// <response code="400">Os parâmetros não foram passados corretamente ou ocorreu algum erro inesperado durante a execução do método</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiBadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto data)
        {
            var result = await _service.CreateAsync(data);
            return ServiceResponse(result, HttpStatusCode.Created);
        }

        /// <summary>
        /// Cancela um Pedido.
        /// </summary>
        /// <param name="id">Id do Pedido</param>
        /// <response code="200">Sucesso na requisição</response>
        /// <response code="400">Os parâmetros não foram passados corretamente ou ocorreu algum erro inesperado durante a execução do método</response>
        [HttpPut("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiBadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel([FromRoute] Guid id)
        {
            await _service.CancelAsync(id);
            return ServiceResponse(HttpStatusCode.OK);
        }
    }
}
