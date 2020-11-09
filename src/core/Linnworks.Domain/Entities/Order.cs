using Linnworks.Core.Domain.Common;
using System;

namespace Linnworks.Core.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Order()
        {
            OrderPriority = new OrderPriority();
        }

        public int Id { get; set; }

        public DateTime OrderedAt { get; set; }

        public OrderPriority OrderPriority { get; set; }

        public Sale Sale { get; set; }
    }
}
