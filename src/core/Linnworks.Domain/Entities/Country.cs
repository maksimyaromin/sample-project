using Linnworks.Core.Domain.Common;
using System.Collections.Generic;

namespace Linnworks.Core.Domain.Entities
{
    public class Country : AuditableEntity
    {
        public Country()
        {
            Region = new Region();
            Sales = new List<Sale>();
        }

        public int Id { get; set; }

        public int RegionId { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }

        public IList<Sale> Sales { get; set; }
    }
}
