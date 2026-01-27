using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.CategoryService
{
    public interface ICategoryService
    {
        Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync();
        Task<Result<string>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Result<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<Result<string>> DeleteCategoryAsync(string id);
        Task<Result<UpdateCategoryDto>> GetByIdCategoryAsync(string id);
    }
}
