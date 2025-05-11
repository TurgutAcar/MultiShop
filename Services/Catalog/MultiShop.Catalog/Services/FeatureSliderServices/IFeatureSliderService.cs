using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto);
        Task DeleteFeatureSliderAsync(string featureSliderId);  
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
