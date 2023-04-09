using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Department { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        [RegularExpression(pattern: @"^[\w!#$%&*+/=?{|}~^-]+(?:\.[\w!#$%&*+/=?{|}~^-]+)*@(?:[A-z0-9-]+\.)+[A-z]{2,6}$",
        ErrorMessage = "Invalid email address.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(pattern: @"^(?=.*[A-Za-z])(?=.*[!#$%&'( )*+,-./:;<=>?@[\]^_`{|}~])[A-Za-z\d!#$%&'( )*+,-./:;<=>?@[\]^_`{|}~]{8,22}$",
        ErrorMessage = "The Password field must contain at least one special character and at least one alphabetic character. Password length should be between 8 and 22 characters")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }

        [Required]
        public RoleValue Role { get; set; }
    }
}
