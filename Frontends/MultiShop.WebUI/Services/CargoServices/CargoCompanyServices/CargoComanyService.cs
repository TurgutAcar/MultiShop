using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<string>> CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto)
        {
            var _httpClient = _factory.Create("Cargo");
           var response =  await _httpClient.PostAsJsonAsync("CargoCompanies", createCargoCompanyDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteCargoCompanyAsync(int id)
        {
            var _httpClient = _factory.Create("Cargo");

           var response = await _httpClient.DeleteAsync("CargoCompanies?id=" + id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultCargoCompanyDto>>> GetAllCargoCompanyAsync()
        {
            var _httpClient = _factory.Create("Cargo");

            var response = await _httpClient.GetAsync("CargoCompanies");
            return await response.ReadSafeResultAsync<List<ResultCargoCompanyDto>>();

        }

        public async Task<Result<UpdateCargoCompanyDto>> GetByIdCargoCompanyAsync(int id)
        {
            var _httpClient = _factory.Create("Cargo");

            var response = await _httpClient.GetAsync("CargoCompanies/" + id);
            return await response.ReadSafeResultAsync<UpdateCargoCompanyDto>();

        }

        public async Task<Result<string>> UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            var _httpClient = _factory.Create("Cargo");

            var response =  await _httpClient.PutAsJsonAsync<UpdateCargoCompanyDto>("CargoCompanies", updateCargoCompanyDto);
            return await response.ReadSafeResultAsync<string>();
        }
    }
}
