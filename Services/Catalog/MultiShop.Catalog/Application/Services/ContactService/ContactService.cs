using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.ContactDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public ContactService(IMapper _mapper,IDatabaseSettings databaseSettings)
        {
            var client=new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _contactCollection = database.GetCollection<Contact>(databaseSettings.ContactCollectionName);
            this._mapper = _mapper;
        }

        public async Task<Result<string>> CreateContactAsync(CreateContactDto createContactDto)
        {
            var value=_mapper.Map<Contact>(createContactDto);
            await _contactCollection.InsertOneAsync(value);
            return "Contact başarıyla oluşturuldu.";
        }

        public async Task<Result<string>> DeleteContactAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x=>x.ContactId==id);
            return "Contact başarıyla silindi.";

        }

        public async Task<Result<List<ResultContactDto>>> GetAllContactAsync()
        {
            var values=await _contactCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultContactDto>>(values); 

        }

        public async Task<Result<GetByIdContactDto>> GetByIdContactAsync(string id)
        {
            var values = await _contactCollection.Find(x => x.ContactId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdContactDto>(values);
        }

        public async Task<Result<string>> UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var value = _mapper.Map<Contact>(updateContactDto);
            await _contactCollection.FindOneAndReplaceAsync(x=>x.ContactId==updateContactDto.ContactId,value);
            return "Contact başarıyla kaydedildi.";

        }
    }
}
