using MultiShop.Shared.Enums;

namespace MultiShop.WebUI.Mapping
{
    public static class UiMessageMapper
    {
        public static string? Map(ResultSource source)
        {
            return source switch
            {
                ResultSource.RateLimited =>
                    "Şu anda yoğunluk var, içerikler biraz gecikebilir.",

                ResultSource.ServiceUnavailable =>
                    "Servise şu anda ulaşılamıyor.",

                ResultSource.Timeout =>
                    "Bağlantı zaman aşımına uğradı.",

                ResultSource.Unauthorized =>
                    "Oturum süren dolmuş olabilir.",

                _ => null
            };
        }
    }

}
