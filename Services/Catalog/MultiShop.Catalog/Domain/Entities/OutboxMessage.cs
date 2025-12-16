using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Domain.Entities
{
    public class OutboxMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; } = default!;
        public string Payload { get; set; } = default!;
        public DateTime OccurredOn { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public string Status { get; set; } = "Pending"; // Pending | Processing | Published | Failed

    }

}
