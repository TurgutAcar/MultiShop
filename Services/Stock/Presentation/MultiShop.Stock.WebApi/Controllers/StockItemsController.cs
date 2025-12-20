using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockItemQueries;

namespace MultiShop.Stock.WebApi.Controllers
{
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
        public async Task<IActionResult> CreateStockItem(CreateStockItemCommand createStockItemCommand)
        {
            var response = await _mediator.Send(createStockItemCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStockItem(UpdateStockItemCommand updateStockItemCommand)
        {
            var response = await _mediator.Send(updateStockItemCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [ValidateAntiForgeryToken] // CSRF Token doğrulamasını zorunlu kılar
        public async Task<IActionResult> RemoveStockItem(int id)
        {
            var response = await _mediator.Send(new RemoveStockItemCommand(id));
            return StatusCode(response.StatusCode, response);

        }
    }
}
