using System.Net.Http.Json;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatologService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;
        public CategoryService(IUiNotifierService uiNotifier, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifier;
            _factory = factory;
        }

        public async Task<Result<string>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", createCategoryDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteCategoryAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("categories?id="+id);
            return await response.ReadSafeResultAsync<string>();

        }
        public async Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync()
        {
            var httpClient = _factory.Create("Catalog");

            var response = await httpClient.GetAsync("categories");

            return await response.ReadSafeResultAsync<List<ResultCategoryDto>>();
        }

        public async Task<Result<UpdateCategoryDto>> GetByIdCategoryAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("categories/" + id);
            return await response.ReadSafeResultAsync<UpdateCategoryDto>();

        }

        public async Task<Result<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories", updateCategoryDto);
            return await response.ReadSafeResultAsync<string>();
        }
    }
}
