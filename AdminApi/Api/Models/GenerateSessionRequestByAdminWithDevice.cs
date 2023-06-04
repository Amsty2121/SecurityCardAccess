using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateSessionRequestByAdminWithDevice
    {
        [Required]
        public Guid AccessCardId { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

		public bool IsFizic { get; set; }
	}
}
