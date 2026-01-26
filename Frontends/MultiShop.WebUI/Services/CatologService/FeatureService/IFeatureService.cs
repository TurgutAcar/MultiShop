
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<Result<List<ResultFeatureDto>>> FeatureListAsync();
        public Task<string> CreateFeatureAsync(CreateFeatureDto feature);
        public Task<string> UpdateFeatureAsync(UpdateFeatureDto feature);
        public Task<string> DeleteFeatureAsync(string featureId);
        public Task<UpdateFeatureDto> GetByIdFeatureAsync(string featureId);
    }
}
