using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.UserIdentityService
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public UserIdentityService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            this._httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<List<ResultUserDto>> GetAllUserListAsync()
        {
            var responseMessage = await _httpClient.GetAsync("/api/users/GetAllUserList");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultUserDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            // var jsonData=await responseMessage.Content.ReadAsStringAsync();
            // var values=JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
            //  return values;
        }
    }
}
