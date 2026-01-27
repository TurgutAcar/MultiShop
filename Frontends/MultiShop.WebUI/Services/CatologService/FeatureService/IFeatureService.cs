
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<Result<List<ResultFeatureDto>>> FeatureListAsync();
        public Task<Result<string>> CreateFeatureAsync(CreateFeatureDto feature);
        public Task<Result<string>> UpdateFeatureAsync(UpdateFeatureDto feature);
        public Task<Result<string>> DeleteFeatureAsync(string featureId);
        public Task<Result<UpdateFeatureDto>> GetByIdFeatureAsync(string featureId);
    }
}
