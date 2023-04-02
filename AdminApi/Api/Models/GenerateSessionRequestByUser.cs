using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateSessionRequestByUser
    {
        [Required]
        public Guid AccessCardId { get; set; }
    }
}
