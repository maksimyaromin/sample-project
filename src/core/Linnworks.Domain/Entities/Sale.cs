using Linnworks.Core.Domain.Common;
using System;

namespace Linnworks.Core.Domain.Entities
{
    public class Sale : AuditableEntity
    {
        public Sale()
        {
            Order = new Order();
            Item = new Item();
            Country = new Country();
        }

        public int Id { get; set; }

        public int OrderId { get; set; }

        public string SalesChannel { get; set; }

        public DateTime? ShippedAt { get; set; }

        public int UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }

        public Order Order { get; set; }    

        public Item Item { get; set; }

        public Country Country { get; set; }
    }
}
