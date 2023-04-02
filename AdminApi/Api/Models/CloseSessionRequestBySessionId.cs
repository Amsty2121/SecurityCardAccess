using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class CloseSessionRequestBySessionId
    {
        [Required]
        public Guid Id { get; set; }
    }
}
