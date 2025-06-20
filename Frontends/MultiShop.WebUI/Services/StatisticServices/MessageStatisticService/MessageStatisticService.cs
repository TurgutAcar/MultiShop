
namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticService
{
    public class MessageStatisticService : IMessageStatisticService
    {
        private readonly HttpClient _httpClient;

        public MessageStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalMessageCount()
        {
            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCount");
            var value=await response.Content.ReadFromJsonAsync<int>();
            return value;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var response = await _httpClient.GetAsync("UserMessages/GetTotalMessageCountByReceiverId?id="+id);
            var value = await response.Content.ReadFromJsonAsync<int>();
            return value;
        }
    }
}
