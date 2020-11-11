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
    public class OrderPrioritiesService : IOrderPrioritiesService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderPrioritiesService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderPriorityDto>> SearchAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.OrderPriorities
                .ProjectTo<OrderPriorityDto>(_mapper.ConfigurationProvider)
                .OrderBy(orderPriority => orderPriority.Symbol)
                .ToListAsync(cancellationToken);
        }
    }
}
