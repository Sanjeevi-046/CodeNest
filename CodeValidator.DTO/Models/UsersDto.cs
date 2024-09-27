using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeValidator.DTO.Models
{
    public class UsersDto
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; // Leave this unset for MongoDB to generate

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("MobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Workspaces { get; set; } = new List<string>();

    }
}
