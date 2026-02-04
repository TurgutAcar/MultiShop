using MultiShop.Shared.Enums;
using System.Net;

namespace MultiShop.WebUI.Mapping
{
    public static class UiMessageMapper
    {
        public static string? Map(int statusCode)
        {
            return statusCode switch
            {
                (int)HttpStatusCode.TooManyRequests =>
                    "Şu anda yoğunluk var, içerikler biraz gecikebilir.",

                (int)HttpStatusCode.ServiceUnavailable =>
                    "Servise şu anda ulaşılamıyor.",

                (int)HttpStatusCode.RequestTimeout =>
                    "Bağlantı zaman aşımına uğradı.",

                (int)HttpStatusCode.Unauthorized =>
                    "Oturum süren dolmuş olabilir.",

                _ => null
            };
        }
    }

}
