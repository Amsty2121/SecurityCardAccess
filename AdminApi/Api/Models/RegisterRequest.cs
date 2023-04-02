using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(22, MinimumLength = 3)]
        [RegularExpression(pattern: @"[A-Z][a-z]+(\s[A-Z][a-z]+)?",
        ErrorMessage = "First Name must be no more than 2 words of alphabetic characters and the first letters must be uppercase.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(22, MinimumLength = 3)]
        [RegularExpression(pattern: @"[A-Z][a-z]+(\s[A-Z][a-z]+)?",
        ErrorMessage = "Last name must be no more than 2 words of alphabetic characters and the first letters must be uppercase.")]
        public string LastName { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        [RegularExpression(pattern: @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
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
