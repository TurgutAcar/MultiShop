using MultiShop.Shared.Responses;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<Result<T>> ReadSafeResultAsync<T>(
    this HttpResponseMessage response)
        {
            // 1️⃣ Success değilse
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;

                // Body varsa al, yoksa default mesaj
                var errorMessage = response.Content != null
                    ? await response.Content.ReadAsStringAsync()
                    : "İstek başarısız oldu.";

                return Result<T>.Failure(statusCode, errorMessage);
            }

            // 2️⃣ Success ise deserialize et
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Result<T>>(json)
                   ?? Result<T>.Failure(500, "Boş response alındı.");
        }

    }

}
