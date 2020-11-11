using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;

namespace Linnworks.Core.Application.Models
{
    public class ItemDto : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
