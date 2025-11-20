using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ContactService
{
    public interface IContactService
    {
        Task<Result<List<ResultContactDto>>> GetAllContactAsync();
        Task<Result<string>> CreateContactAsync(CreateContactDto createContactDto);
        Task<Result<string>> UpdateContactAsync(UpdateContactDto updateContactDto);
        Task<Result<string>> DeleteContactAsync(string id);
        Task<Result<GetByIdContactDto>> GetByIdContactAsync(string id);
    }
}
