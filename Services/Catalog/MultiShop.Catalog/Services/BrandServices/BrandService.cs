using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;

namespace MultiShop.Catalog.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            this._mapper = mapper;
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _brandCollection = database.GetCollection<Brand>(databaseSettings.BrandCollectionName);
        }

        public async Task<List<ResultBrandDto>> BrandListAsync()
        {
            var values=await _brandCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBrandDto>>(values);
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var map=_mapper.Map<Brand>(createBrandDto);
            await _brandCollection.InsertOneAsync(map);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _brandCollection.DeleteOneAsync(x=>x.BrandId==id);
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
        {
            var value = await _brandCollection.Find(x => x.BrandId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBrandDto>(value);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var map = _mapper.Map<Brand>(updateBrandDto);
            await _brandCollection.FindOneAndReplaceAsync(x=>x.BrandId==updateBrandDto.BrandId, map);
        }
    }
}
