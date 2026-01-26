using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync();
        Task<string> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto);
        Task<string> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto);
        Task<string> DeleteFeatureSliderAsync(string featureSliderId);  
        Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId);
      
    }
}
