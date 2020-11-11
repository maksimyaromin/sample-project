using AutoMapper;
using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;
using System;

namespace Linnworks.Core.Application.Models
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }

        public DateTime OrderedAt { get; set; }

        public int OrderPriorityId { get; set; }

        public string OrderPrioritySymbol { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(
                    d => d.OrderPriorityId,
                    opt => opt.MapFrom(s => s.OrderPriority.Id))
                .ForMember(
                    d => d.OrderPrioritySymbol,
                    opt => opt.MapFrom(s => s.OrderPriority.Symbol));
        }
    }
}
