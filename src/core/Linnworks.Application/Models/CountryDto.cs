using AutoMapper;
using Linnworks.Core.Application.Common.Mappings;
using Linnworks.Core.Domain.Entities;

namespace Linnworks.Core.Application.Models
{
    public class CountryDto : IMapFrom<Country>
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public string Name { get; set; }

        public string RegionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDto>()
                .ForMember(
                    d => d.RegionName,
                    opt => opt.MapFrom(s => s.Region.Name)
                );
        }
    }
}
