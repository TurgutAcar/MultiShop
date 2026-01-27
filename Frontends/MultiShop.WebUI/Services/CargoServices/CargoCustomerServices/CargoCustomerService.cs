using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public CargoCustomerService(IApiClientFactory factory, IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<Result<List<ResultCargoCustomerDto>>> GetByCargoCustomerListAsync(string id)

        {
            var _httpClient = _factory.Create("Cargo");
            var response = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerListById?id=" + id);
            return await response.ReadSafeResultAsync<List<ResultCargoCustomerDto>>();

        }
        public async Task<Result<GetCargoCustomerByIdDto>> GetByIdCargoCustomerInfoAsync(string id)
        {
            var _httpClient = _factory.Create("Cargo");

            var response = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerById?id/" + id);
            return await response.ReadSafeResultAsync<GetCargoCustomerByIdDto>();

        }
    }
}
