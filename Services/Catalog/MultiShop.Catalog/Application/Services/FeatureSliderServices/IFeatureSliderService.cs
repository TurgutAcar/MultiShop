using MultiShop.Catalog.Application.Dtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync();
        Task<Result<string>> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto);
        Task<Result<string>> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto);
        Task<Result<string>> DeleteFeatureSliderAsync(string featureSliderId);  
        Task<Result<GetByIdFeatureSliderDto>> GetByIdFeatureSliderAsync(string featureSliderId);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
