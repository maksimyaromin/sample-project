using AutoMapper;
using AutoMapper.QueryableExtensions;
using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services
{
    public class RegionsService : IRegionsService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public RegionsService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RegionDto>> SearchAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Regions
                .ProjectTo<RegionDto>(_mapper.ConfigurationProvider)
                .OrderBy(region => region.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
