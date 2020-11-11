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
    public class CountriesService : ICountriesService
    {
        private readonly ILinnworksDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountriesService(
            ILinnworksDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> AutocompleteAsync(AutocompleteCriteria autocompleteCriteria, CancellationToken cancellationToken)
        {
            return await _dbContext.Countries
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .Where(country => EF.Functions.Like(country.Name, $"%{autocompleteCriteria.Query}%"))
                .OrderBy(country => country.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateAsync(CountryDto country, CancellationToken cancellationToken)
        {
            var entity = new Country
            {
                Name = country.Name,
                RegionId = country.RegionId
            };

            _dbContext.Countries.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task DeleteAsync(int countryId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Countries.FindAsync(countryId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), countryId);
            }

            _dbContext.Countries.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<CountryDto> GetAsync(int countryId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Countries
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(
                    country => country.Id == countryId,
                    cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), countryId);
            }

            return entity;
        }

        public async Task UpdateAsync(int countryId, CountryDto country, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Countries.FindAsync(countryId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), countryId);
            }

            entity.Name = country.Name;
            entity.RegionId = country.RegionId;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
