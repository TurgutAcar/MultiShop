using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
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

        public async Task<List<ResultCargoCustomerDto>> GetByCargoCustomerListAsync(string id)

        {
            var _httpClient = _factory.Create("Cargo");
            var response = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerListById?id=" + id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultCargoCustomerDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultCargoCustomerDto>();
            //  var jsonData = await response.Content.ReadAsStringAsync();

            // var value = JsonConvert.DeserializeObject<List<ResultCargoCustomerDto>>(jsonData);

            // return value;

        }
        public async Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id)
        {
            var _httpClient = _factory.Create("Cargo");

            var responseMessage = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerById?id/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<GetCargoCustomerByIdDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new GetCargoCustomerByIdDto();
            //  var values= await responseMessage.Content.ReadFromJsonAsync<GetCargoCustomerByIdDto>();
            // return values;
        }
    }
}
