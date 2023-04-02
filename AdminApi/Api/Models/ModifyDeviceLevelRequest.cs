using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class ModifyDeviceLevelRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public AccessLevel AccessLevel { get; set; }
    }
}
