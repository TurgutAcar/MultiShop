using MultiShop.Shared.Responses;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interface;
using System.Net;

namespace MultiShop.WebUI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<UserDetailViewModel>> GetUserInfo()
        {
            var response = await _httpClient.GetAsync("/api/users/getuser");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return (StatusCodes.Status401Unauthorized, "Oturum süreniz doldu");
             
            }

            if (!response.IsSuccessStatusCode)
            {
                return (StatusCodes.Status401Unauthorized, "Beklenmeyen bir hata oluştu");
               
            }
            var result = await response.Content.ReadFromJsonAsync<Result<UserDetailViewModel>>();
            return result;


            //return await _httpClient.GetFromJsonAsync<UserDetailViewModel>("/api/users/getuser");
        }
    }
}
