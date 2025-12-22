using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;
        public AddressesController(RemoveAddressCommandHandler removeAddressCommandHandler,UpdateAddressCommandHandler updateAddressCommandHandler,CreateAddressCommandHandler createAddressCommandHandler,GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddressCommandHandler = updateAddressCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var response = await _getAddressQueryHandler.Handle();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var response = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand createAddressCommand)
        {
            var response=await _createAddressCommandHandler.Handle(createAddressCommand);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            var response=await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand updateAddressCommand)
        {
            var response=await _updateAddressCommandHandler.Handle(updateAddressCommand);
            return StatusCode(response.StatusCode, response);
        }
    }

}
