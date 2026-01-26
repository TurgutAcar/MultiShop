using MultiShop.WebUI.Enums;

namespace MultiShop.WebUI.Services.Interface
{
    public interface IApiClientFactory
    {
        public HttpClient Create(
    string serviceName,
    ApiClientPolicy? policy = null);
    }

}
