using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private HttpClient _httpClient;

        public CargoCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCargoCustomerDto>> GetByCargoCustomerListAsync(string id)

        {

            var response = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerListById?id=" + id);

            var jsonData = await response.Content.ReadAsStringAsync();

            var value = JsonConvert.DeserializeObject<List<ResultCargoCustomerDto>>(jsonData);

            return value;

        }
        public async Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerById?id/" + id);
            var values= await responseMessage.Content.ReadFromJsonAsync<GetCargoCustomerByIdDto>();
            return values;
        }
    }
}
