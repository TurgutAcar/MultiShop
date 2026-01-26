using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public class CargoComanyService : ICargoComanyService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public CargoComanyService(IApiClientFactory factory, IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<string> CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto)
        {
            var _httpClient = _factory.Create("Cargo");
           var responseMessage=  await _httpClient.PostAsJsonAsync("CargoCompanies", createCargoCompanyDto);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteCargoCompanyAsync(int id)
        {
            var _httpClient = _factory.Create("Cargo");

           var responseMessage= await _httpClient.DeleteAsync("CargoCompanies?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync()
        {
            var _httpClient = _factory.Create("Cargo");

            var responseMessage = await _httpClient.GetAsync("CargoCompanies");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultCargoCompanyDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultCargoCompanyDto>();
            //var contentValue = await responseMessage.Content.ReadAsStringAsync();
            // var value = JsonConvert.DeserializeObject<List<ResultCargoCompanyDto>>(contentValue);
            //  return value;
        }

        public async Task<UpdateCargoCompanyDto> GetByIdCargoCompanyAsync(int id)
        {
            var _httpClient = _factory.Create("Cargo");

            var responseMessage = await _httpClient.GetAsync("CargoCompanies/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateCargoCompanyDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateCargoCompanyDto();
            // var contentValue = await responseMessage.Content.ReadAsStringAsync();
            //  var value = JsonConvert.DeserializeObject<UpdateCargoCompanyDto>(contentValue);
            // return value;
        }

        public async Task<string> UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            var _httpClient = _factory.Create("Cargo");

           var responseMessage =  await _httpClient.PutAsJsonAsync<UpdateCargoCompanyDto>("CargoCompanies", updateCargoCompanyDto);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
