

using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<string>> CreateContactAsync(CreateContactDto createContactDto)
        {
            var _httpClient = _factory.Create("Catalog");
            var response =await _httpClient.PostAsJsonAsync<CreateContactDto>("Contacts", createContactDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteContactAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.DeleteAsync("Contacts?id="+id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultContactDto>>> GetAllContactAsync()
        {
            var _httpClient = _factory.Create("Catalog");

            var response = await _httpClient.GetAsync("Contacts");
            return await response.ReadSafeResultAsync<List<ResultContactDto>>();

        }

        public async Task<Result<UpdateContactDto>> GetByIdContactAsync(string id)
        {
            var _httpClient = _factory.Create("Catalog");
            var response = await _httpClient.GetAsync("Contacts/" + id);
            return await response.ReadSafeResultAsync<UpdateContactDto>();

        }

        public async Task<Result<string>> UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var _httpClient = _factory.Create("Catalog");

            var response =await _httpClient.PutAsJsonAsync<UpdateContactDto>("Contacts", updateContactDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
