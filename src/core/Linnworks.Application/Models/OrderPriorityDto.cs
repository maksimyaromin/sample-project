using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;

namespace Linnworks.Core.Application.Models
{
    public class OrderPriorityDto : IMapFrom<OrderPriority>
    {
        public int Id { get; set; }

        public string Symbol { get; set; }
    }
}
