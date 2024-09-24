using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CodeValidator.DAL.Models
{
    public class User
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

        [BsonElement("Register-Date")]
        public string RegisterDate { get; set; } = DateTime.Now.ToString();

        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("MobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

    }
}
