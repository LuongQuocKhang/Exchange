using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Exchange.Data.Entities
{
    public class Signals
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Symbol")]
        public string Symbol { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Indicator")]
        public string Indicator { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
