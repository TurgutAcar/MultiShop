

using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.WebUI.Services.ContactService;
using Newtonsoft.Json;

namespace MultiShop.Catalog.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            await _httpClient.PostAsJsonAsync<CreateContactDto>("Contacts", createContactDto);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync("Contacts?id="+id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Contacts");
            var contentValue = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(contentValue);
            return values;
        }

        public async Task<UpdateContactDto> GetByIdContactAsync(string id)
        {
            var responseMessage =await _httpClient.GetAsync("Contacts/" + id);
            var contentValue=await responseMessage.Content.ReadAsStringAsync();
            var value=JsonConvert.DeserializeObject<UpdateContactDto>(contentValue);
            return value;
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        { 
            await _httpClient.PutAsJsonAsync<UpdateContactDto>("Contacts", updateContactDto);
        }
    }
}
