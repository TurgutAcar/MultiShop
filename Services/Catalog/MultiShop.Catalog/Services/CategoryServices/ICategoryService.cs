using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync();
        Task<Result<string>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Result<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<Result<string>> DeleteCategoryAsync(string id);
        Task<Result<GetByIdCategoryDto>> GetByIdCategoryAsync(string id);
        
    }
}
