using System;

namespace Linnworks.Core.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAtUtc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public string UpdatedAt { get; set; }
    }
}
