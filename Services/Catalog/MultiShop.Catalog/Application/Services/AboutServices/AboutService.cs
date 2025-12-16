using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.AboutDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<About> _aboutCollection;

        public AboutService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _aboutCollection=database.GetCollection<About>(databaseSettings.AboutCollectionName);
        }

        public async Task<Result<List<ResultAboutDto>>> AboutListAsync()
        {
            var values =await _aboutCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultAboutDto>>(values);
        }

        public async Task<Result<string>> CreateAboutAsync(CreateAboutDto createAboutDto)
        {
           var map=_mapper.Map<About>(createAboutDto);
            await _aboutCollection.InsertOneAsync(map);
            return "About oluşturuldu.";
        }

        public async Task<Result<string>> DeleteAboutAsync(string id)
        {
            await _aboutCollection.DeleteOneAsync(x=>x.AboutId==id);
            return "About silindi.";

        }

        public async Task<Result<GetByIdAboutDto>> GetByIdAboutAsync(string id)
        {
            var values = await _aboutCollection.Find(x => x.AboutId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdAboutDto>(values);
        }

        public async Task<Result<string>> UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var map = _mapper.Map<About>(updateAboutDto);
            await _aboutCollection.FindOneAndReplaceAsync(x=>x.AboutId==updateAboutDto.AboutId,map);
            return "About kaydedildi.";

        }
    }
}
