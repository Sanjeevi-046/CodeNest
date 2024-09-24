using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CodeValidator.DTO.Models
{
    public class UserDto
    {
        
        [BsonId]
       
        public string Id { get; set; } = string.Empty;
        
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name must be alphabetic characters.")]
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be minimum 8 characters, at least one uppercase letter, one lowercase letter, one number and one special character !")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address !")]
        public string? Email { get; set; }

        public string? RegisterDate { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "FirstName must be alphabetic characters.")]
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "LastName must be alphabetic characters.")]
        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "MobileNumber must be exactly 10 digits.")]
        [Required(ErrorMessage = "MobileNumber is required")]
        public string? MobileNumber { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\s]{1,50}$", ErrorMessage = "Country must be up to 50 alphabetic characters.")]
        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }

    }
}
