
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
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

        public async Task<Result<int>> GetActiveCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            return await response.ReadSafeResultAsync<int>();

        }

        public async Task<Result<int>> GetPassiveCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
            return await response.ReadSafeResultAsync<int>();

        }

        public async Task<Result<int>> GetTotalCommentCount()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            return await response.ReadSafeResultAsync<int>();

        }
    }
}
