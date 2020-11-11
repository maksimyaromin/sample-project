using AutoMapper;
using AutoMapper.QueryableExtensions;
using Linnworks.Core.Application.Common.Exceptions;
using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task DeleteManyAsync(int[] saleIds, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Sales
                .Where(sale => saleIds.Contains(sale.Id))
                .ToListAsync(cancellationToken);

            if (entities.Count != saleIds.Length)
            {
                throw new NotFoundException(nameof(Sale), saleIds);
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

        public async Task ImportAsync(IEnumerable<SaleDto> sales, CancellationToken cancellationToken)
        {
            var itemKeys = sales
                .Select(sale => sale.ItemName)
                .Distinct();
            var countryKeys = sales
                .Select(sale => sale.CountryName)
                .Distinct();
            var regionKeys = sales
                .Select(sale => sale.CountryRegionName)
                .Distinct();
            var orderPriorityKeys = sales
                .Select(sale => sale.OrderPrioritySymbol)
                .Distinct();

            var items = new Dictionary<string, Item>();
            var countries = new Dictionary<string, Country>();
            var regions = new Dictionary<string, Region>();
            var orderPriorities = new Dictionary<string, OrderPriority>();

            async Task itemsRead()
            {
                items = await _dbContext.Items
                    .Where(item => itemKeys.Contains(item.Name))
                    .ToDictionaryAsync(item => item.Name);
            }

            async Task countriesRead()
            {
                countries = await _dbContext.Countries
                    .Where(country => countryKeys.Contains(country.Name))
                    .ToDictionaryAsync(country => country.Name);
            }

            async Task regionsRead()
            {
                regions = await _dbContext.Regions
                    .Where(region => regionKeys.Contains(region.Name))
                    .ToDictionaryAsync(region => region.Name);
            }

            async Task orderPrioritiesRead()
            {
                orderPriorities = await _dbContext.OrderPriorities
                    .Where(orderPriority => orderPriorityKeys.Contains(orderPriority.Symbol))
                    .ToDictionaryAsync(orderPriority => orderPriority.Symbol);
            }

            await Task.WhenAll(new Task[]
            {
                itemsRead(),
                countriesRead(),
                regionsRead(),
                orderPrioritiesRead()
            });


            var entities = new List<Sale>();

            foreach (var sale in sales)
            {
                Item item;
                Country country;
                Region region;
                OrderPriority orderPriority;

                if (items.ContainsKey(sale.ItemName))
                {
                    item = items[sale.ItemName];
                }
                else
                {
                    item = new Item
                    {
                        Id = 0,
                        Name = sale.ItemName
                    };
                    items.Add(sale.ItemName, item);
                }

                if (regions.ContainsKey(sale.CountryRegionName))
                {
                    region = regions[sale.CountryRegionName];
                }
                else
                {
                    region = new Region
                    {
                        Id = 0,
                        Name = sale.CountryRegionName
                    };
                    regions.Add(sale.CountryRegionName, region);
                }

                if (countries.ContainsKey(sale.CountryName))
                {
                    country = countries[sale.CountryName];

                    if (country.RegionId != region.Id)
                    {
                        country.Id = 0;
                        country.RegionId = region.Id;
                        country.Region = region;
                    }
                }
                else
                {
                    country = new Country
                    {
                        Id = 0,
                        Name = sale.CountryName,
                        RegionId = region.Id,
                        Region = region
                    };
                    countries.Add(sale.CountryName, country);
                }

                if (orderPriorities.ContainsKey(sale.OrderPrioritySymbol))
                {
                    orderPriority = orderPriorities[sale.OrderPrioritySymbol];
                }
                else
                {
                    orderPriority = new OrderPriority
                    {
                        Id = 0,
                        Symbol = sale.OrderPrioritySymbol
                    };
                    orderPriorities.Add(sale.OrderPrioritySymbol, orderPriority);
                }

                var order = new Order
                {
                    OrderedAt = sale.OrderedAt,
                    OrderPriority = orderPriority
                };

                entities.Add(new Sale
                {
                    SalesChannel = sale.SalesChannel,
                    ShippedAt = sale.ShippedAt,
                    UnitsSold = sale.UnitsSold,
                    UnitPrice = sale.UnitPrice,
                    UnitCost = sale.UnitCost,
                    TotalRevenue = sale.TotalRevenue,
                    TotalCost = sale.TotalCost,
                    TotalProfit = sale.TotalProfit,
                    Order = order,
                    Item = item,
                    Country = country
                });
            }

            _dbContext.Sales.AddRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<SaleDto>> SearchAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sales
                .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(sale => sale.OrderedAt)
                .Skip((searchCriteria.CurrentPage - 1) * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize)
                .ToListAsync();
        }

        public async Task<SearchOptions> SearchOptionsAsync(CancellationToken cancellationToken)
        {
            return new SearchOptions
            {
                Total = await _dbContext.Sales.CountAsync()
            };
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
