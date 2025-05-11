using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
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

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }
        [HttpGet]
        public IActionResult GetCargoOperationList() 
        {
            var values = _cargoOperationService.TGetAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            CargoOperation cargoOperation=new CargoOperation()
            {
                Barcode= createCargoOperationDto.Barcode,
                Description= createCargoOperationDto.Description,
                OperationDate= createCargoOperationDto.OperationDate,
            };
            _cargoOperationService.TInsert(cargoOperation);
            return Ok("CargoOperation başarıyla oluşturuldu.");
        }
        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
                CargoOperationId= updateCargoOperationDto.CargoOperationId,
                Barcode = updateCargoOperationDto.Barcode,
                Description = updateCargoOperationDto.Description,
                OperationDate = updateCargoOperationDto.OperationDate,
            };
            _cargoOperationService.TUpdate(cargoOperation);
            return Ok("CargoOperation başarıyla güncellendi.");
        }
        [HttpDelete]
        public IActionResult DeleteCargoOperation(int id)
        {
            _cargoOperationService.TDelete(id); 
            return Ok("CargoOperation başarıyla silindi.");
        }
    }
}
