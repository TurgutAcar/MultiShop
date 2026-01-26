
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.UserStatisticServices
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public UserStatisticService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            _httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<int> GetUserCount()
        {
            var response =await _httpClient.GetAsync("Statistics");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            // var jsonData=await response.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<int>(jsonData);
            //  return values;
        }
    }
}
