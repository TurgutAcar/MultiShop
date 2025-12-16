using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.SpecialOfferServices
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

        public async Task<Result<string>> CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var data=_mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferCollection.InsertOneAsync(data);
            return "Speccial Offer oluşturuldu.";
        }

        public async Task<Result<string>> DeleteSpecialOfferAsync(string id)
        {
            await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);
            return "Speccial Offer silindi.";


        }

        public async Task<Result<List<ResultSpecialOfferDto>>> GetAllSpecialOfferAsync()
        {
            var value = await _specialOfferCollection.Find(x => true).ToListAsync();
            var map = _mapper.Map<List<ResultSpecialOfferDto>>(value);
            return map;
        }

        public async Task<Result<GetByIdSpecialOfferDto>> GetByIdSpecialOfferAsync(string id)
        {
            var value=await  _specialOfferCollection.Find(x=>x.SpecialOfferId==id).FirstOrDefaultAsync();
            var map=_mapper.Map<GetByIdSpecialOfferDto>(value);
            return map;
        }

        public async Task<Result<string>> UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var data = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);

            await _specialOfferCollection.FindOneAndReplaceAsync(x=>x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, data);
            return "Speccial Offer kaydedildi.";

        }
    }
}
