using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Dtos.FeatureDtos
{
    public class CreateFeatureDto
    {
      
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
