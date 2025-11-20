using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;
        private readonly IMapper _mapper;


        public CargoCustomersController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            _cargoCustomerService = cargoCustomerService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult CargoCustomerList()
        {
            var response = _cargoCustomerService.TGetAll();
            return StatusCode(response.StatusCode, response);
           
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var response = _cargoCustomerService.TGetById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            var map = _mapper.Map<CargoCustomer>(createCargoCustomerDto);

            var response = _cargoCustomerService.TInsert(map);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
           

            var map = _mapper.Map<CargoCustomer>(updateCargoCustomerDto);

            var response = _cargoCustomerService.TUpdate(map);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult DeleteCargoCustomer(int id)
        {
            var response=_cargoCustomerService.TDelete(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetCargoCustomerById")]
        public IActionResult GetCargoCustomerById(string id)
        {
            var response = _cargoCustomerService.TGetCargoCustomerById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetCargoCustomerListById")]
        public IActionResult GetCargoCustomerListById(string id)
        {
            var response = _cargoCustomerService.TGetCargoCustomerListById(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
