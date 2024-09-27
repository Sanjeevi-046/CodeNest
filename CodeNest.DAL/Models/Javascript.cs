using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DAL.Models
{
    public class Javascript
    {

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string JavascriptInput { get; set; }
        public string JavascriptOutput { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModifiedBy { get; set; }
        public string? ModifiedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Workspaces { get; set; }

        public string Version { get; set; }
    }
}
