using AutoMapper;
using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;
using System;

namespace Linnworks.Core.Application.Models
{
    public class SaleDto : IMapFrom<Sale>
    {
        public int Id { get; set; }

        public string SalesChannel { get; set; }

        public DateTime? ShippedAt { get; set; }

        public int UnitsSold { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal TotalProfit { get; set; }

        public DateTime OrderedAt { get; set; }

        public int OrderId { get; set; }

        public int OrderPriorityId { get; set; }

        public string OrderPrioritySymbol { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int CountryId { get; set; }

        public int CountryRegionId { get; set; }

        public string CountryName { get; set; }

        public string CountryRegionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sale, SaleDto>()
                .ForMember(
                    d => d.OrderedAt,
                    opt => opt.MapFrom(s => s.Order.OrderedAt))
                .ForMember(
                    d => d.OrderPriorityId,
                    opt => opt.MapFrom(s => s.Order.OrderPriority.Id))
                .ForMember(
                    d => d.OrderPrioritySymbol,
                    opt => opt.MapFrom(s => s.Order.OrderPriority.Symbol))
                .ForMember(
                    d => d.ItemId,
                    opt => opt.MapFrom(s => s.Item.Id))
                .ForMember(
                    d => d.ItemName,
                    opt => opt.MapFrom(s => s.Item.Name))
                .ForMember(
                    d => d.CountryId,
                    opt => opt.MapFrom(s => s.Country.Id))
                .ForMember(
                    d => d.CountryRegionId,
                    opt => opt.MapFrom(s => s.Country.RegionId))
                .ForMember(
                    d => d.CountryName,
                    opt => opt.MapFrom(s => s.Country.Name)
                )
                .ForMember(
                    d => d.CountryRegionName,
                    opt => opt.MapFrom(s => s.Country.Region.Name));

            profile.CreateMap<SaleDto, Sale>()
                .ForMember(
                    d => d.Order,
                    opt => opt.MapFrom(s => new Order
                    {
                        Id = s.OrderId,
                        OrderedAt = s.OrderedAt,
                        OrderPriority = new OrderPriority
                        {
                            Id = s.OrderPriorityId,
                            Symbol = s.OrderPrioritySymbol
                        }
                    }))
                .ForMember(
                    d => d.Item,
                    opt => opt.MapFrom(s => new Item
                    {
                        Id = s.ItemId,
                        Name = s.ItemName
                    }))
                .ForMember(
                    d => d.Country,
                    opt => opt.MapFrom(s => new Country
                    {
                        Id = s.CountryId,
                        Name = s.CountryName,
                        RegionId = s.CountryRegionId,
                        Region = new Region
                        {
                            Id = s.CountryRegionId,
                            Name = s.CountryRegionName
                        }
                    }));
        }
    }
}
