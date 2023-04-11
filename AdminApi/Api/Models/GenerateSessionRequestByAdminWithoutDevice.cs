using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateSessionRequestByAdminWithoutDevice
    {
        [Required]
        public Guid AccessCardId { get; set; }
    }
}
