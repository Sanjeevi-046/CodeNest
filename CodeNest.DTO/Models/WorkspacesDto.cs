using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CodeNest.DTO.Models
{
    public class WorkspacesDto
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [RegularExpression(@"^[A-Za-z\d\s]{5,}$", ErrorMessage = "Surname must be alphabetic characters and Numeric.")]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedOn { get; set; }

    }
}
