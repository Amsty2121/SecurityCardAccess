using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class GenerateDeviceRequest
    {
        [Required]
        [MaxLength(512)]
        public string? Description { get; set; }

        [Required]
        public AccessLevel AccessLevel { get; set; }
    }
}
