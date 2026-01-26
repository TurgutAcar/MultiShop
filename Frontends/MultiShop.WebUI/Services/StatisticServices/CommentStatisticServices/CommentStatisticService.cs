
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices
{
    public class CommentStatisticService : ICommentStatisticService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public CommentStatisticService(HttpClient httpClient, IUiNotifierService uiNotifierService, IApiClientFactory factory)
        {
            _uiNotifierService = uiNotifierService;
            _factory = factory;
        }

        public async Task<int> GetActiveCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            //var jsonData=await response.Content.ReadAsStringAsync();
            // var value = JsonConvert.DeserializeObject<int>(jsonData);
            // return value;
        }

        public async Task<int> GetPassiveCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
            //   var jsonData = await response.Content.ReadAsStringAsync();
            //  var value = JsonConvert.DeserializeObject<int>(jsonData);
            //  return value;
        }

        public async Task<int> GetTotalCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<int>>(jsonData);
            return values.HandleUiResult(_uiNotifierService);
          //  var jsonData = await response.Content.ReadAsStringAsync();
          //  var value = JsonConvert.DeserializeObject<int>(jsonData);
          //  return value;
        }
    }
}
