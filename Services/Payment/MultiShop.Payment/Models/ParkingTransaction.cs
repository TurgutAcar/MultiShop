namespace MultiShop.Payment.Models
{
    using System.Text.Json.Serialization;

    public class ParkingTransaction
    {
        [JsonPropertyName("corporateCode")]
        public string CorporateCode { get; set; }

        [JsonPropertyName("carParkCode")]
        public string CarParkCode { get; set; }

        [JsonPropertyName("vehicleClass")]
        public int VehicleClass { get; set; }

        [JsonPropertyName("entryLaneNo")]
        public int EntryLaneNo { get; set; }

        [JsonPropertyName("entryTimestamp")]
        public DateTime EntryTimestamp { get; set; }

        [JsonPropertyName("exitLaneNo")]
        public int ExitLaneNo { get; set; }

        [JsonPropertyName("exitTimestamp")]
        public DateTime ExitTimestamp { get; set; }

        [JsonPropertyName("parkDuration")]
        public int ParkDuration { get; set; }

        [JsonPropertyName("transactionDescription")]
        public string TransactionDescription { get; set; }

        [JsonPropertyName("plateNo")]
        public string PlateNo { get; set; }

        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }

        [JsonPropertyName("corporateReferenceNo")]
        public string CorporateReferenceNo { get; set; }
    }
}
