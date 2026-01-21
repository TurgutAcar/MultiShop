using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.Interface
{
    public interface IIdentityService
    {
        public Task<Result<List<string>>> SignIn(SignInDto signUpDto);
        public Task<bool> GetRefreshToken();



    }
}
