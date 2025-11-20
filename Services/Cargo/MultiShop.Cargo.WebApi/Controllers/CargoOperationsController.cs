using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.DtoLayer.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private ICargoOperationService _cargoOperationService;
        private readonly IMapper _mapper;

        public CargoOperationsController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            _cargoOperationService = cargoOperationService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCargoOperationList() 
        {
            var response = _cargoOperationService.TGetAll();
            return StatusCode(response.StatusCode, response);
          
        }
        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
          
            var map = _mapper.Map<CargoOperation>(createCargoOperationDto);

            var response = _cargoOperationService.TInsert(map);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
          
            var map = _mapper.Map<CargoOperation>(updateCargoOperationDto);

            var response = _cargoOperationService.TUpdate(map);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult DeleteCargoOperation(int id)
        {
            var response = _cargoOperationService.TDelete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
