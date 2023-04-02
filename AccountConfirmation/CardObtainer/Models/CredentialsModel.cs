using System.ComponentModel.DataAnnotations;

namespace CardObtainer.Dto
{
    public class CredentialsModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
