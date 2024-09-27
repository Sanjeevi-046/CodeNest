using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DAL.Models
{
    public class BaseToString
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string BaseInput { get; set; }
        public string StringOutput { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModifiedBy { get; set; }
        public string? ModifiedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Workspaces { get; set; }

        public string Version { get; set; } // 0.0.0 For each update we need to increment - 0.0.1
    }
}
