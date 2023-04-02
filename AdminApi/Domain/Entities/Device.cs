using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Device : BaseEntity
    {
        [Required]
        [MaxLength(512)]
        public string? Description { get; set; }

        [Required]
        public AccessLevel AccessLevel { get; set; }

    }
}
