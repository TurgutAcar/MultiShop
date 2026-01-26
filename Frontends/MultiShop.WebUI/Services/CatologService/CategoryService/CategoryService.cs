using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        //  private readonly HttpClient _httpClient;
        private readonly IApiClientFactory _factory;

        private readonly IUiNotifierService _uiNotifierService;
        public CategoryService(IUiNotifierService uiNotifier, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifier;
            _factory = factory;
        }

        public async Task<string> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", createCategoryDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteCategoryAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("categories?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
        public async Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync()
        {
            var httpClient = _factory.Create("Catalog");

            var response = await httpClient.GetAsync("categories");

            // Extension metodun bunu zaten map’liyor
            return await response.ReadSafeResultAsync<List<ResultCategoryDto>>();
        }

        //    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        //    {
        //        var _httpClient = _factory.Create("Catalog");

        //        var responseMessage = await _httpClient.GetAsync("categories");

        //        var result =
        //    await responseMessage.ReadSafeResultAsync<List<ResultCategoryDto>>();

        //        return result?.HandleUiResult(_uiNotifierService)
        //       ?? new List<ResultCategoryDto>();

        //       // var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //       // var values = JsonConvert.DeserializeObject<Result<List<ResultCategoryDto>>>(jsonData);
        //       // return values.HandleUiResult(_uiNotifierService)
        ////   ?? new List<ResultCategoryDto>();

        //      //  var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        //      //  return values;
        //    }

        public async Task<UpdateCategoryDto> GetByIdCategoryAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage =await _httpClient.GetAsync("categories/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateCategoryDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateCategoryDto();
            //var values=await responseMessage.Content.ReadFromJsonAsync<UpdateCategoryDto>();
            //return values;

        }

        public async Task<string> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories", updateCategoryDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
