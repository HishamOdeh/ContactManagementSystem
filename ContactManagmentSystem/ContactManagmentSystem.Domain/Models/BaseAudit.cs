using System.ComponentModel.DataAnnotations;

namespace ContactManagementSystem.Domain.Models
{
    public abstract class BaseAudit
    {
        [Required]
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}