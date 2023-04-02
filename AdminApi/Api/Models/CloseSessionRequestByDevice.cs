using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    public class CloseSessionRequestByCardId
    {
        [Required]
        public string PassCode { get; set; }

        [Required]
        public Guid DeviceId { get; set; }
        
    }
}
