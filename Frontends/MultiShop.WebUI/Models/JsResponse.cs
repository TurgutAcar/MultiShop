namespace MultiShop.WebUI.Models
{
    public class JsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // "success", "error", "warning", "info"

        // İhtiyaç duyarsan ek veri dönmek için (ID, yeni URL vb.)
        public object Data { get; set; }

        // Yardımcı static metotlar kullanımı kolaylaştırır
        public static JsResponse Error(string message) =>
            new() { Success = false, Message = message, Type = "error" };

        public static JsResponse Ok(string message, object data = null) =>
            new() { Success = true, Message = message, Type = "success", Data = data };
    }
}
