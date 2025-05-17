using MultiShop.DtoLayer.IdentityDtos.LoginDtos;

namespace MultiShop.WebUI.Services.Interface
{
    public interface IIdentityService
    {
        public Task<bool> SignIn(SignInDto signUpDto);
        public Task<bool> GetRefreshToken();



    }
}
