using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.BrandDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _brandCollection = database.GetCollection<Brand>(databaseSettings.BrandCollectionName);
        }

        public async Task<Result<List<ResultBrandDto>>> BrandListAsync()
        {
            var values=await _brandCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultBrandDto>>(values);
        }

        public async Task<Result<string>> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var map=_mapper.Map<Brand>(createBrandDto);
            await _brandCollection.InsertOneAsync(map);
            return "Brand oluşturuldu.";
        }

        public async Task<Result<string>> DeleteBrandAsync(string id)
        {
            await _brandCollection.DeleteOneAsync(x=>x.BrandId==id);
            return "Brand silindi.";

        }

        public async Task<Result<GetByIdBrandDto>> GetByIdBrandAsync(string id)
        {
            var value = await _brandCollection.Find(x => x.BrandId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdBrandDto>(value);
        }

        public async Task<Result<string>> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var map = _mapper.Map<Brand>(updateBrandDto);
            await _brandCollection.FindOneAndReplaceAsync(x=>x.BrandId==updateBrandDto.BrandId, map);
            return "Brand kaydedildi.";

        }
    }
}
