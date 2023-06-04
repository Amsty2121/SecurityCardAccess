using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Session : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AccessCardId { get; set; }

        public Guid? DeviceId { get; set; }

        [Required]
        public SessionStatus SessionStatus { get; set; }

        [Required]
        public DateTime StartUtcDate { get; set; }
        
        public DateTime? UsedUtcDate { get; set; } = null;

        [Required]
        public DateTime EndUtcDate { get; set; }
    }
}
