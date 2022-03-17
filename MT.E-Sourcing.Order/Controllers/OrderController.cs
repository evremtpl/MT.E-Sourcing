using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Order.Application.Commands.OrderCreate;
using MT.E_Sourcing.Order.Application.Queries;
using MT.E_Sourcing.Order.Application.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("GetOrdersByUserName/userName")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersBySellerUserNameQuery(userName);

            var orders = await _mediator.Send(query);

            if (!orders.Any()) return NotFound();

            return Ok(orders);
        }



        public async Task<ActionResult<OrderResponse>> OrderCreate([FromBody] OrderCreateCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
