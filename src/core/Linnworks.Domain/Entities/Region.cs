using Linnworks.Core.Domain.Common;
using System.Collections.Generic;

namespace Linnworks.Core.Domain.Entities
{
    public class Region : AuditableEntity
    {
        public Region()
        {
            Countries = new List<Country>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Country> Countries { get; set; }
    }
}