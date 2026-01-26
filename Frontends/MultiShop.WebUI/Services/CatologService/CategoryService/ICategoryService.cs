using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.CategoryService
{
    public interface ICategoryService
    {
        Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync();
        Task<string> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<string> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<string> DeleteCategoryAsync(string id);
        Task<UpdateCategoryDto> GetByIdCategoryAsync(string id);
    }
}
