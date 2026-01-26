
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.ContactService
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<string> CreateContactAsync(CreateContactDto createContactDto);
        Task<string> UpdateContactAsync(UpdateContactDto updateContactDto);
        Task<string> DeleteContactAsync(string id);
        Task<UpdateContactDto> GetByIdContactAsync(string id);
    }
}
