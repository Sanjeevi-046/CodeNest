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

       
    }
}
