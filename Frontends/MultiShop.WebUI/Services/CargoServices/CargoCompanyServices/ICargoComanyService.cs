using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoComanyService
    {
        Task<Result<List<ResultCargoCompanyDto>>> GetAllCargoCompanyAsync();
        Task<Result<string>> CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto);
        Task<Result<string>> UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto);
        Task<Result<string>> DeleteCargoCompanyAsync(int id);
        Task<Result<UpdateCargoCompanyDto>> GetByIdCargoCompanyAsync(int id);

    }
}
