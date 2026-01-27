using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.UserIdentityService
{
    public interface IUserIdentityService
    {
        Task<Result<List<ResultUserDto>>> GetAllUserListAsync();
    }
}
