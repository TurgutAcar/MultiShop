using Newtonsoft.Json;

namespace MultiShop.SignalRRealTime.Api.Services.SignalRCommentServices
{
    public class SignalRCommentService:ISignalRCommentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SignalRCommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetTotalCommentCount()
        {
            var client =_httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("comments/GetTotalCommentCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value=JsonConvert.DeserializeObject<int>(jsonData);
            return value;
        }

    }
}
