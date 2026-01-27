using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<Result<GetCargoCustomerByIdDto>> GetByIdCargoCustomerInfoAsync(string id);
        Task<Result<List<ResultCargoCustomerDto>>> GetByCargoCustomerListAsync(string id);


    }
}
