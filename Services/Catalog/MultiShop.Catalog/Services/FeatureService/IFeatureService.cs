using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.FeatureService
{
    public interface IFeatureService
    {
        public Task<List<ResultFeatureDto>> FeatureListAsync();
        public Task CreateFeatureAsync(CreateFeatureDto feature);
        public Task UpdateFeatureAsync(UpdateFeatureDto feature);
        public Task DeleteFeatureAsync(string featureId);
        public Task<GetByIdFeatureDto> GetByIdFeatureAsync(string featureId);
    }
}
