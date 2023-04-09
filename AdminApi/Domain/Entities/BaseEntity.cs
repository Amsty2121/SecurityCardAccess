using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public abstract class BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
    }
}
