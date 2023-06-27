using System;

namespace ContactManagementSystem.Domain.Models
{
    public abstract class BaseEntity : BaseAudit
    {
        public Guid Id { get; set; }
    }
}
