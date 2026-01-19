using MultiShop.Shared.Responses;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.Interface
{
    public interface IUserService
    {
        public Task<Result<UserDetailViewModel>> GetUserInfo();
    }
}
