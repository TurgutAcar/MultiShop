using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries;

namespace MultiShop.Stock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockReservationsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> StockReservationList()
        {
            var response = await _mediator.Send(new GetStockReservationQuery());
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockReservationgById(int id)
        {
            var response = await _mediator.Send(new GetStockReservationByIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
        public async Task<IActionResult> CreateStockReservation(CreateStockReservationCommand createStockReservationCommand)
        {
            var response = await _mediator.Send(createStockReservationCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStockReservation(UpdateStockReservationCommand updateStockReservationCommand)
        {
            var response = await _mediator.Send(updateStockReservationCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        [ValidateAntiForgeryToken] // CSRF Token doğrulamasını zorunlu kılar
        public async Task<IActionResult> RemoveStockReservation(int id)
        {
            var response = await _mediator.Send(new RemoveStockReservationCommand(id));
            return StatusCode(response.StatusCode, response);

        }
    }
}
