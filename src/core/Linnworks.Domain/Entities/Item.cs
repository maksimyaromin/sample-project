using Linnworks.Core.Domain.Common;
using System.Collections.Generic;

namespace Linnworks.Core.Domain.Entities
{
    public class Item : AuditableEntity
    {
        public Item()
        {
            Sales = new List<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Sale> Sales { get; set; }
    }
}
