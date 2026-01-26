using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Services.Interface;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ApiClientFactory : IApiClientFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClientFactory(IHttpContextAccessor contextAccessor, IHttpClientFactory httpClientFactory)
        {
            _contextAccessor = contextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient Create(string serviceName, ApiClientPolicy? policy = null)
        {
            string clientName = "";
            var effectivePolicy = policy ?? ResolvePolicyFromContext();
            if (effectivePolicy == ApiClientPolicy.Authorized)
                clientName = $"{serviceName}Authorized";
            else
                clientName = $"{serviceName}Visitor";

            //var clientName = effectivePolicy switch
            //    {
            //        ApiClientPolicy.Visitor => $"{serviceName}Visitor",
            //        ApiClientPolicy.Authorized => $"{serviceName}Customer",
            //        _ => $"{serviceName}Visitor"
            //    };

            return _httpClientFactory.CreateClient(clientName);
        }

        private ApiClientPolicy ResolvePolicyFromContext()
        {
            var user = _contextAccessor.HttpContext?.User;
           // user!.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"); // true
           // user!.IsInRole("Admin"); // true ✅


            //if (user?.IsInRole("Admin") == true)
            //    return ApiClientPolicy.Admin;

            if (user?.Identity?.IsAuthenticated == true)
                return ApiClientPolicy.Authorized;

            return ApiClientPolicy.Visitor;
        }

    }
}
