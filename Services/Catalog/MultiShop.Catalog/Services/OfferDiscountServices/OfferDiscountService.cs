using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private IMapper _mapper;
        private IMongoCollection<OfferDiscount> _offerDiscountCollection;
        public OfferDiscountService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _offerDiscountCollection=database.GetCollection<OfferDiscount>(databaseSettings.OfferDiscountCollectionName);
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var map=_mapper.Map<OfferDiscount>(createOfferDiscountDto);
            await _offerDiscountCollection.InsertOneAsync(map);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _offerDiscountCollection.DeleteOneAsync(x=>x.OfferDiscountId==id);
        }

        public async Task<List<ResultOfferDiscountDto>> OfferDiscountListAsync()
        {
            var value = await _offerDiscountCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultOfferDiscountDto>>(value);
        }

        public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var value= await _offerDiscountCollection.Find(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOfferDiscountDto>(value);
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var map = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
            await _offerDiscountCollection.FindOneAndReplaceAsync(x => x.OfferDiscountId == updateOfferDiscountDto.OfferDiscountId, map);
        }
    }
}
