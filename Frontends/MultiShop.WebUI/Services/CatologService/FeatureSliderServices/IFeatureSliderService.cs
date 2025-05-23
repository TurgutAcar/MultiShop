using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;

namespace MultiShop.WebUI.Services.CatologService.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto);
        Task DeleteFeatureSliderAsync(string featureSliderId);  
        Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId);
      
    }
}
