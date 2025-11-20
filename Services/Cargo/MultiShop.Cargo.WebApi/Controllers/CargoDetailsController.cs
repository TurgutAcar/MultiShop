using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.DtoLayer.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;
        private readonly IMapper _mapper;

        public CargoDetailsController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            _cargoDetailService = cargoDetailService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult CargoDetailList()
        {
            var response = _cargoDetailService.TGetAll();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public IActionResult GetCargoDetailById(int id)
        {
            var response = _cargoDetailService.TGetById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult DeleteCargoDetail(int id)
        {
            var response = _cargoDetailService.TDelete(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
         
            var map = _mapper.Map<CargoDetail>(createCargoDetailDto);

            var response = _cargoDetailService.TInsert(map);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
           
            var map = _mapper.Map<CargoDetail>(updateCargoDetailDto);

            var response = _cargoDetailService.TUpdate(map);
            return StatusCode(response.StatusCode, response);

        }
    }
}
