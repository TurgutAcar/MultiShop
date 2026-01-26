

using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.ContactService;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.Catalog.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;
        public ContactService(IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<string> CreateContactAsync(CreateContactDto createContactDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateContactDto>("Contacts", createContactDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteContactAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("Contacts?id="+id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var responseMessage = await _httpClient.GetAsync("Contacts");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultContactDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultContactDto>();
            //  var contentValue = await responseMessage.Content.ReadAsStringAsync();
            //  var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(contentValue);
            //  return values;
        }

        public async Task<UpdateContactDto> GetByIdContactAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var responseMessage =await _httpClient.GetAsync("Contacts/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateContactDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateContactDto();
            // var contentValue=await responseMessage.Content.ReadAsStringAsync();
            //   var value=JsonConvert.DeserializeObject<UpdateContactDto>(contentValue);
            //  return value;
        }

        public async Task<string> UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateContactDto>("Contacts", updateContactDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
