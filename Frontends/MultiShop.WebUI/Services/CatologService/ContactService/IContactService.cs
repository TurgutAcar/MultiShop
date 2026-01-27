
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.ContactService
{
    public interface IContactService
    {
        Task<Result<List<ResultContactDto>>> GetAllContactAsync();
        Task<Result<string>> CreateContactAsync(CreateContactDto createContactDto);
        Task<Result<string>> UpdateContactAsync(UpdateContactDto updateContactDto);
        Task<Result<string>> DeleteContactAsync(string id);
        Task<Result<UpdateContactDto>> GetByIdContactAsync(string id);
    }
}
