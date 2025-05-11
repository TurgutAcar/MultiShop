using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            this._mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(databaseSettings.FeatureSliderCollectionName);

        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto)
        {
            var values=_mapper.Map<FeatureSlider>(featureSliderDto);
            await _featureSliderCollection.InsertOneAsync(values);
        }

        public async Task DeleteFeatureSliderAsync(string featureSliderId)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == featureSliderId);


        }

        public async Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();

        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {

            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(values);
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string featureSliderId)
        {
            var value =await _featureSliderCollection.Find(x=>x.FeatureSliderId == featureSliderId).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDto>(value);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto)
        {
            var value=_mapper.Map<FeatureSlider>(featureSliderDto);
            await _featureSliderCollection.FindOneAndReplaceAsync(x => x.FeatureSliderId == featureSliderDto.FeatureSliderId, value);

        }

      
    }
}
