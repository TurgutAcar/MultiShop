using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;

namespace MultiShop.Catalog.Services.FeatureService
{
    public class FeatureService : IFeatureService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Feature> _featureCollection;
        public FeatureService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _featureCollection = database.GetCollection<Feature>(databaseSettings.FeatureCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto feature)
        {
            var map=_mapper.Map<Feature>(feature);
            await _featureCollection.InsertOneAsync(map);
        }

        public async Task DeleteFeatureAsync(string featureId)
        {
            await _featureCollection.DeleteOneAsync(x=>x.FeatureId== featureId);
        }

        public async Task<List<ResultFeatureDto>> FeatureListAsync()
        {
            var values=await _featureCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureDto>>(values);
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string featureId)
        {
            var values = await _featureCollection.Find(x => x.FeatureId == featureId).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureDto>(values);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            var map = _mapper.Map<Feature>(updateFeatureDto);
            await _featureCollection.FindOneAndReplaceAsync(x=>x.FeatureId== updateFeatureDto.FeatureId,map);
        }
    }
}
