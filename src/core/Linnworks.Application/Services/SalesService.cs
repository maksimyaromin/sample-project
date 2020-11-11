using AutoMapper;
using AutoMapper.QueryableExtensions;
using Linnworks.Core.Application.Common.Exceptions;
using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public SalesService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(SaleDto sale, CancellationToken cancellationToken)
        {
            var entity = MapFromDto(sale);

            _dbContext.Sales.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task DeleteManyAsync(int[] salesId, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Sales
                .Where(sale => salesId.Contains(sale.Id))
                .ToListAsync(cancellationToken);

            if (entities.Count != salesId.Length)
            {
                throw new NotFoundException(nameof(Sale), salesId);
            }

            _dbContext.Sales.RemoveRange(entities);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<SaleDto> GetAsync(int saleId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Sales
                .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(
                    sale => sale.Id == saleId,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Sale), saleId);
            }

            return entity;
        }

        public Task ImportAsync(IEnumerable<SaleDto> sales, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<SearchQueryResult<SaleDto>> SearchAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
        {
            var sales = _dbContext.Sales
                .ProjectTo<SaleDto>(_mapper.ConfigurationProvider);

            return SearchQueryResult<SaleDto>.CreateAsync(sales, searchCriteria);
        }

        public async Task UpdateAsync(int saleId, SaleDto sale, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Sales.FindAsync(saleId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Sale), saleId);
            }

            var newEntity = MapFromDto(sale);

            entity.OrderId = newEntity.OrderId;
            entity.SalesChannel = newEntity.SalesChannel;
            entity.ShippedAt = newEntity.ShippedAt;
            entity.UnitsSold = newEntity.UnitsSold;
            entity.UnitPrice = newEntity.UnitPrice;
            entity.UnitCost = newEntity.UnitCost;
            entity.TotalRevenue = newEntity.TotalRevenue;
            entity.TotalCost = newEntity.TotalCost;
            entity.TotalProfit = newEntity.TotalProfit;
            entity.Order = newEntity.Order;
            entity.Item = newEntity.Item;
            entity.Country = newEntity.Country;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private Sale MapFromDto(SaleDto dto)
        {
            return _mapper.Map<Sale>(dto);
        }
    }
}
