using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(databaseSettings.FeatureSliderCollectionName);

        }

        public async Task<Result<string>> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSliderDto)
        {
            var values=_mapper.Map<FeatureSlider>(featureSliderDto);
            await _featureSliderCollection.InsertOneAsync(values);
            return "FeatureSlider oluşturuldu";

        }

        public async Task<Result<string>> DeleteFeatureSliderAsync(string featureSliderId)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == featureSliderId);
            return "FeatureSlider silindi";

        }

        public  Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();

        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<ResultFeatureSliderDto>>> GetAllFeatureSliderAsync()
        {

            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(values);
        }

        public async Task<Result<GetByIdFeatureSliderDto>> GetByIdFeatureSliderAsync(string featureSliderId)
        {
            var value =await _featureSliderCollection.Find(x=>x.FeatureSliderId == featureSliderId).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDto>(value);
        }

        public async Task<Result<string>> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSliderDto)
        {
            var value=_mapper.Map<FeatureSlider>(featureSliderDto);
            await _featureSliderCollection.FindOneAndReplaceAsync(x => x.FeatureSliderId == featureSliderDto.FeatureSliderId, value);
            return "FeatureSlider kaydedildi";

        }


    }
}
