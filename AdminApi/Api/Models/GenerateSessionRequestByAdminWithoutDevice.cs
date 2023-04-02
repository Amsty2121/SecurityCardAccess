using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateSessionRequestByAdminWithoutDevice
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AccessCardId { get; set; }
    }
}
