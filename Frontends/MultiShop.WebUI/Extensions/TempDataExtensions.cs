using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Models;
using System.Text.Json;
namespace MultiShop.WebUI.Extensions
{
  

    public static class TempDataExtensions
    {
        public static void SetUiMessage(this ITempDataDictionary tempData, UiMessage message)
        {
            tempData["UiMessage"] = JsonSerializer.Serialize(message);
        }

        public static UiMessage? GetUiMessage(this ITempDataDictionary tempData)
        {
            if (!tempData.ContainsKey("UiMessage"))
                return null;

            var raw = tempData["UiMessage"]?.ToString();
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            // JSON mu?
            if (raw.TrimStart().StartsWith("{"))
            {
                return JsonSerializer.Deserialize<UiMessage>(raw);
            }

            // Düz string ise default Success kabul et
            return new UiMessage
            {
                Type = UiMessageType.Success,
                Message = raw
            };
        }

    }

}
