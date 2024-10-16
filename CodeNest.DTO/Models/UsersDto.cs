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
        public ObjectId Id { get; set; }
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name must be alphabetic characters.")]
        [Required(ErrorMessage = "Name is required")]
        [BsonElement("Name")]
        public string Name { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be minimum 8 characters, at least one uppercase letter, one lowercase letter, one number and one special character !")]
        [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Email")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address !")]
        public string Email { get; set; }

        [BsonElement("FirstName")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "FirstName must be alphabetic characters.")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "LastName must be alphabetic characters.")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [BsonElement("MobileNumber")]
        [StringLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "MobileNumber must be exactly 10 digits.")]
        [Required(ErrorMessage = "MobileNumber is required")]
        public string MobileNumber { get; set; }

        [BsonElement("Country")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Country must be up to 50 alphabetic characters.")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<ObjectId>? Workspaces { get; set; }
    }
}