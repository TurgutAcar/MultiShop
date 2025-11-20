using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<Result<List<ResultFeatureDto>>> FeatureListAsync();
        public Task<Result<string>> CreateFeatureAsync(CreateFeatureDto feature);
        public Task<Result<string>> UpdateFeatureAsync(UpdateFeatureDto feature);
        public Task<Result<string>> DeleteFeatureAsync(string featureId);
        public Task<Result<GetByIdFeatureDto>> GetByIdFeatureAsync(string featureId);
    }
}
