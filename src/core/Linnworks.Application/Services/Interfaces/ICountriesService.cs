using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<IEnumerable<CountryDto>> AutocompleteAsync(AutocompleteCriteria autocompleteCriteria, CancellationToken cancellationToken);

        Task<CountryDto> GetAsync(int countryId, CancellationToken cancellationToken);

        Task<int> CreateAsync(CountryDto country, CancellationToken cancellationToken);

        Task UpdateAsync(int countryId, CountryDto country, CancellationToken cancellationToken);

        Task DeleteAsync(int countryId, CancellationToken cancellationToken);
    }
}
