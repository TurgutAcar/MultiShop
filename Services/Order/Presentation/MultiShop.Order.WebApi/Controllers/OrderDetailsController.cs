using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailByIdQuery;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailQueryByIdHandler _getOrderDetailQueryByIdHandler;
        private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
        private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
        private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;

        public OrderDetailsController(UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler,CreateOrderDetailCommandHandler createOrderDetailCommandHandler,GetOrderDetailQueryHandler getOrderDetailQueryHandler,GetOrderDetailQueryByIdHandler getOrderDetailQueryByIdHandler,RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
        {
            _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
            _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            _getOrderDetailQueryByIdHandler = getOrderDetailQueryByIdHandler;
            _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
           var values= await _getOrderDetailQueryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var response = await _getOrderDetailQueryByIdHandler.Handle(
                new GetOrderDetailByIdQuery(id));
            return StatusCode(response.StatusCode,response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand createOrderDetailCommand)
        {
           var response= await _createOrderDetailCommandHandler.Handle(createOrderDetailCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveOrderDetail(int id)
        {
            var response=await _removeOrderDetailCommandHandler.Handle(
                new RemoveOrderDetailCommand(id));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
           var response= await _updateOrderDetailCommandHandler.Handle(updateOrderDetailCommand);
            return StatusCode(response.StatusCode, response);
        }
    }  
}
