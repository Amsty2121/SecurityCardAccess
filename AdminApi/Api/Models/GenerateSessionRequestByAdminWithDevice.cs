using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateSessionRequestByAdminWithDevice
    {
        [Required]
        public Guid AccessCardId { get; set; }

        public Guid DeviceId { get; set; }
    }
}
