using AutoMapper;
using Linnworks.Core.Application.Common.Exceptions;
using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Linnworks.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrdersService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task UpdateAsync(int orderId, OrderDto order, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Orders
                .FindAsync(orderId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Order), orderId);
            }

            entity.OrderedAt = order.OrderedAt;
            entity.OrderPriority = new OrderPriority
            {
                Id = order.OrderPriorityId,
                Symbol = order.OrderPrioritySymbol
            };

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
