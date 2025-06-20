
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices
{
    public class CommentStatisticService : ICommentStatisticService
    {
        private readonly HttpClient _httpClient;

        public CommentStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetActiveCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            var jsonData=await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<int>(jsonData);
            return value;
        }

        public async Task<int> GetPassiveCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<int>(jsonData);
            return value;
        }

        public async Task<int> GetTotalCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            var jsonData = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<int>(jsonData);
            return value;
        }
    }
}
