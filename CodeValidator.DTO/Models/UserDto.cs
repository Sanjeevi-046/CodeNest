using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeValidator.DTO.Models
{
    public class UserDto
    {
        
            [BsonId]
            [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; } = string.Empty; 

            [BsonElement("Name")]
            public string Name { get; set; }

            [BsonElement("Password")]
            public string Password { get; set; }

    }
}
