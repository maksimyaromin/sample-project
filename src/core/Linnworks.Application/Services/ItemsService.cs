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
    public class ItemsService : IItemsService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemsService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> AutocompleteAsync(AutocompleteCriteria autocompleteCriteria, CancellationToken cancellationToken)
        {
            return await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .Where(item => EF.Functions.Like(item.Name, $"%{autocompleteCriteria.Query}%"))
                .OrderBy(item => item.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateAsync(ItemDto item, CancellationToken cancellationToken)
        {
            var entity = new Item
            {
                Name = item.Name
            };

            _dbContext.Items.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task DeleteAsync(int itemId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(itemId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), itemId);
            }

            _dbContext.Items.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ItemDto> GetAsync(int itemId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(
                    item => item.Id == itemId,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), itemId);
            }

            return entity;
        }

        public async Task UpdateAsync(int itemId, ItemDto item, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Items.FindAsync(itemId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), itemId);
            }

            entity.Name = item.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
