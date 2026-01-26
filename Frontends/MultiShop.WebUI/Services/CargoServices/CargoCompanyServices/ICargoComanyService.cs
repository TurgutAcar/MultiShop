using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoComanyService
    {
        Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync();
        Task<string> CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto);
        Task<string> UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto);
        Task<string> DeleteCargoCompanyAsync(int id);
        Task<UpdateCargoCompanyDto> GetByIdCargoCompanyAsync(int id);

    }
}
