
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<List<ResultFeatureDto>> FeatureListAsync();
        public Task CreateFeatureAsync(CreateFeatureDto feature);
        public Task UpdateFeatureAsync(UpdateFeatureDto feature);
        public Task DeleteFeatureAsync(string featureId);
        public Task<UpdateFeatureDto> GetByIdFeatureAsync(string featureId);
    }
}
