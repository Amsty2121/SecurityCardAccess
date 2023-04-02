using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class LoginRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Login length should be be between 8 and 30 characters")]

        public string Login { get; set; }

        [Required]
        [StringLength(22, MinimumLength = 8, ErrorMessage = "Password length should be be between 8 and 22 characters")]
        public string Password { get; set; }
    }
}
