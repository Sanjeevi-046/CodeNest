using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeValidator.DTO.Models
{
    public class XmlDto
    {

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string XmlInput { get; set; }
        public string XmlOutput { get; set; }
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
