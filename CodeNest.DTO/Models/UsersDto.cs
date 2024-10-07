// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: Sample Description.
//
// ***********************************************************************************************

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CodeNest.DTO.Models
{
    public class UsersDto
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }  // Leave this unset for MongoDB to generate

        [Required(ErrorMessage = "Name is required")]
        [BsonElement("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Email")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "MobileNumber is required")]
        [BsonElement("MobileNumber")]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Country is required")]
        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<ObjectId> Workspaces { get; set; } = new List<ObjectId>();
    }
}
