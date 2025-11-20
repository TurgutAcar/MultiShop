using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly IMapper _mapper;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            _cargoCompanyService = cargoCompanyService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult CargoCompanyList()
        {
            var response = _cargoCompanyService.TGetAll();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyById(int id)
        {
            var response = _cargoCompanyService.TGetById(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        public IActionResult CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
          
            var map = _mapper.Map<CargoCompany>(createCargoCompanyDto);

            var response=_cargoCompanyService.TInsert(map);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
           
            var map = _mapper.Map<CargoCompany>(updateCargoCompanyDto);

            var response = _cargoCompanyService.TUpdate(map);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public IActionResult DeleteCargoCompany(int id)
        {
            var response = _cargoCompanyService.TDelete(id);
            return StatusCode(response.StatusCode, response);
        }


    }
}
