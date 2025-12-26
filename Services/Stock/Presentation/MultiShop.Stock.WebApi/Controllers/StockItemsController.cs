using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockItemQueries;
using MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands;

namespace MultiShop.Stock.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockItemsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> StockItemList()
        {
            var response = await _mediator.Send(new GetStockItemQuery());
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockItemgById(int id)
        {
            var response = await _mediator.Send(new GetStockItemByIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStockItem(CreateStockItemCommand createStockItemCommand)
        {
            var response = await _mediator.Send(createStockItemCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateStockItem(UpdateStockItemCommand updateStockItemCommand)
        {
            var response = await _mediator.Send(updateStockItemCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("increase")]
        public async Task<IActionResult> IncreaseStockItem(IncreaseStockItemCommand increaseStockItemCommand)
        {
            var response = await _mediator.Send(increaseStockItemCommand);
            return StatusCode(response.StatusCode, response);
        }
        //[HttpPut("decrease")]
        //public async Task<IActionResult> DecreaseStockItem(RollbackStockItemCommand decreaseStockItemCommand)
        //{
        //    var response = await _mediator.Send(decreaseStockItemCommand);
        //    return StatusCode(response.StatusCode, response);
        //}
        [HttpDelete]
        public async Task<IActionResult> DeactivateStockItem(int id,int type)
        {
            var response = await _mediator.Send(new DeactivateStockItemCommand(id,type));
            return StatusCode(response.StatusCode, response);

        }
    }
}
