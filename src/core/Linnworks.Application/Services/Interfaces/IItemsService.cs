using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services.Interfaces
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemDto>> AutocompleteAsync(AutocompleteCriteria autocompleteCriteria, CancellationToken cancellationToken);

        Task<ItemDto> GetAsync(int itemId, CancellationToken cancellationToken);

        Task<int> CreateAsync(ItemDto item, CancellationToken cancellationToken);

        Task UpdateAsync(int itemId, ItemDto item, CancellationToken cancellationToken);

        Task DeleteAsync(int itemId, CancellationToken cancellationToken);
    }
}
