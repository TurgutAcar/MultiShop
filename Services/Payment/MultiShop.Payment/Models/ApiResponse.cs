using System.Text.Json.Serialization;

namespace MultiShop.Payment.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("result")]
        public ProvisionResult Result { get; set; }
    }
    public class ProvisionResult
    {
        [JsonPropertyName("corporateCode")]
        public string CorporateCode { get; set; }

        [JsonPropertyName("corporateReferenceNo")]
        public string CorporateReferenceNo { get; set; }

        [JsonPropertyName("plate")]
        public string Plate { get; set; }

        [JsonPropertyName("provisionReferenceNo")]
        public string ProvisionReferenceNo { get; set; }

        [JsonPropertyName("provisionSonuc")]
        public int ProvisionSonuc { get; set; }

        [JsonPropertyName("provisionDescription")]
        public string ProvisionDescription { get; set; }

        [JsonPropertyName("provisionFee")]
        public decimal ProvisionFee { get; set; }
    }
}
