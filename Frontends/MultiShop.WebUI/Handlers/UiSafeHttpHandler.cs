//using MultiShop.Shared.Responses;
//using MultiShop.WebUI.Services.NotifierServices;
//using Newtonsoft.Json;
//using System.Net;
//using System.Text;


//namespace MultiShop.WebUI.Handlers
//{
//    public class UiSafeHttpHandler : DelegatingHandler
//    {
//        private readonly IUiNotifierService _uiNotifier;

//        public UiSafeHttpHandler(IUiNotifierService uiNotifier)
//        {
//            _uiNotifier = uiNotifier;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(
//            HttpRequestMessage request,
//            CancellationToken cancellationToken)
//        {
//            var response = await base.SendAsync(request, cancellationToken);

//            if (response.StatusCode == HttpStatusCode.TooManyRequests)
//            {
//                _uiNotifier.Info(
//                    "Şu anda yoğunluk var, içerikler biraz gecikebilir.");

//                return CreateEmptySuccessResponse(response);
//            }

//            return response;
//        }

//        private static HttpResponseMessage CreateEmptySuccessResponse(
//            HttpResponseMessage original)
//        {
//            return new HttpResponseMessage(HttpStatusCode.OK)
//            {
//                RequestMessage = original.RequestMessage,
//                Content = new StringContent(
//                JsonConvert.SerializeObject(
//                    Result<List<object>>.Succeed(new List<object>())
//                ),
//                Encoding.UTF8,
//                "application/json"
//)

//            };
//        }
//    }

//}
