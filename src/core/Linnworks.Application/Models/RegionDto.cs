using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;

namespace Linnworks.Core.Application.Models
{
    public class RegionDto : IMapFrom<Region>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
