using Linnworks.Core.Domain.Common;
using System.Collections.Generic;

namespace Linnworks.Core.Domain.Entities
{
    public class OrderPriority : AuditableEntity
    {
        public OrderPriority()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Symbol { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
