using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockTransactionsQueries;

namespace MultiShop.Services.Stock.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockTransactionsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> StockTransactionList()
        {
            var response = await _mediator.Send(new GetStockTransactionQuery());
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockTransactiongById(int id)
        {
            var response = await _mediator.Send(new GetStockTransactionByIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
        public async Task<IActionResult> CreateStockTransaction(CreateStockTransactionCommand createStockTransactionCommand)
        {
            var response = await _mediator.Send(createStockTransactionCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStockTransaction(UpdateStockTransactionCommand updateStockTransactionCommand)
        {
            var response = await _mediator.Send(updateStockTransactionCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [ValidateAntiForgeryToken] // CSRF Token doğrulamasını zorunlu kılar
        public async Task<IActionResult> RemoveStockTransaction(int id)
        {
            var response = await _mediator.Send(new RemoveStockTransactionCommand(id));
            return StatusCode(response.StatusCode, response);

        }
    }
}
