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
            var statusCode = (int)response.StatusCode;
            var body = response.Content == null
                ? null
                : await response.Content.ReadAsStringAsync();

            // ❌ HTTP ERROR
            if (!response.IsSuccessStatusCode)
            {
                if (!string.IsNullOrWhiteSpace(body))
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<Result<T>>(body);
                        if (result != null)
                            return result;
                    }
                    catch { /* ignore */ }
                }

                return Result<T>.Failure(statusCode, UiMessageMapper.Map(statusCode));
            }

            // ✅ SUCCESS BUT EMPTY BODY
            if (string.IsNullOrWhiteSpace(body))
                return Result<T>.Failure(500, "Boş response alındı");

            // ✅ Try deserialize Result<T>
            try
            {
                var result = JsonConvert.DeserializeObject<Result<T>>(body);
                if (result != null)
                    return result;
            }
            catch { }

            // ✅ Fallback: Direct T payload (no Result wrapper)
            try
            {
                var data = JsonConvert.DeserializeObject<T>(body);
                return Result<T>.Succeed(data);
            }
            catch
            {
                return Result<T>.Failure(500, "Response parse edilemedi");
            }
        }
    }

    //public static class HttpResponseExtensions
    //{
    //    public static async Task<Result<T>> ReadSafeResultAsync<T>(
    //this HttpResponseMessage response)
    //    {
    //        // 1️⃣ Success değilse
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            Result<T>? result = null;

    //            var statusCode = (int)response.StatusCode;

    //            // Body varsa al, yoksa default mesaj
    //            if (response.Content != null)
    //            {
    //                var body = await response.Content.ReadAsStringAsync();
    //                result = JsonConvert.DeserializeObject<Result<T>>(body);

    //              //  result = JsonSerializer.Deserialize<Result<T>>(body);
    //                return result ?? Result<T>.Failure(statusCode, UiMessageMapper.Map(statusCode));

    //            }
    //            //var errorMessage = response.Content != null
    //            //    ? await response.Content.ReadAsStringAsync()
    //            //    : "İstek başarısız oldu.";

    //        //    return Result<T>.Failure(statusCode, "İstek başarısız oldu.");
    //        }

    //        // 2️⃣ Success ise deserialize et
    //        var json = await response.Content.ReadAsStringAsync();

    //        return JsonConvert.DeserializeObject<Result<T>>(json)
    //               ?? Result<T>.Failure(500, "Boş response alındı.");
    //    }

    //}

}
