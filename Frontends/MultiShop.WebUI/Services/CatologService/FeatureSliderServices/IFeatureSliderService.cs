using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync();
        Task<Result<string>> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto);
        Task<Result<string>> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto);
        Task<Result<string>> DeleteFeatureSliderAsync(string featureSliderId);  
        Task<Result<UpdateFeatureSliderDto>> GetByIdFeatureSliderAsync(string featureSliderId);
      
    }
}
