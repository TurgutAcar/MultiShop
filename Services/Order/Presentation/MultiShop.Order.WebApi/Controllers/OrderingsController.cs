using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var response =await _mediator.Send(new GetOrderingQuery());
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderingById(int id)
        {
            var response =await _mediator.Send(new GetOrderingByIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
        //[HttpPost]
      
        //public async Task<IActionResult> CreateOrdering(CreateOrderingCommand createOrderingCommand)
        //{
        //    var response = await _mediator.Send(createOrderingCommand);
        //    return StatusCode(response.StatusCode, response);
        //}
        [HttpPut]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand updateOrderingCommand)
        {
            var response = await _mediator.Send(updateOrderingCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [ValidateAntiForgeryToken] // CSRF Token doğrulamasını zorunlu kılar
        public async Task<IActionResult> RemoveOrdering(int id)
        {
            var response = await _mediator.Send(new RemoveOrderingCommand(id));
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("GetOrderingByUserId/{id}")]
        public async Task<IActionResult> GetOrderingByUserId(string id)
        {
            var response = await _mediator.Send(new GetOrderingByUserIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
    }
}
