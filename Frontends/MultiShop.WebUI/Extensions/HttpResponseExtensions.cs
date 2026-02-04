using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
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
                Result<T>? result = null;

                var statusCode = (int)response.StatusCode;

                // Body varsa al, yoksa default mesaj
                if (response.Content != null)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Result<T>>(body);

                  //  result = JsonSerializer.Deserialize<Result<T>>(body);
                    return result ?? Result<T>.Failure(statusCode, UiMessageMapper.Map(statusCode));
                    
                }
                //var errorMessage = response.Content != null
                //    ? await response.Content.ReadAsStringAsync()
                //    : "İstek başarısız oldu.";

            //    return Result<T>.Failure(statusCode, "İstek başarısız oldu.");
            }

            // 2️⃣ Success ise deserialize et
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Result<T>>(json)
                   ?? Result<T>.Failure(500, "Boş response alındı.");
        }

    }

}
