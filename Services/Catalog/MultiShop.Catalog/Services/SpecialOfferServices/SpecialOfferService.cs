using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;

        public SpecialOfferService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _specialOfferCollection = database.GetCollection<SpecialOffer>(databaseSettings.SpecialOfferCollectionName);
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var data=_mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(data);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);

        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var value = await _specialOfferCollection.Find<SpecialOffer>(x => true).ToListAsync();
            var map = _mapper.Map<List<ResultSpecialOfferDto>>(value);
            return map;
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var value=await  _specialOfferCollection.Find(x=>x.SpecialOfferId==id).FirstOrDefaultAsync();
            var map=_mapper.Map<GetByIdSpecialOfferDto>(value);
            return map;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var data = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);

            await _specialOfferCollection.FindOneAndReplaceAsync(x=>x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, data);
        }
    }
}
