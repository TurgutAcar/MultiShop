using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Dtos.FeatureDtos
{
    public class GetByIdFeatureDto
    {
        public string FeatureId { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
