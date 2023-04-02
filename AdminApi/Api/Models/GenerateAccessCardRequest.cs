using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateAccessCardRequest
    {
        [Required]
        public string PassCode { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public AccessLevel AccessLevel { get; set; }

        [MaxLength(512)]
        public string? Description { get; set; }
    }
}
