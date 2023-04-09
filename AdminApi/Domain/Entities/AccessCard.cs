using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AccessCard : BaseEntity
    {
        [Required]
        public string PassCode { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public AccessLevel AccessLevel { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime UpdateUtcDate { get; set; }
        public DateTime? LastUsingUtcDate { get; set; }
    }
}
