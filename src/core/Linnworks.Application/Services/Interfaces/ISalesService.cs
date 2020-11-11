using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<SaleDto>> SearchAsync(SearchCriteria searchCriteria, CancellationToken cancellationToken);

        Task<SearchOptions> SearchOptionsAsync(CancellationToken cancellationToken);

        Task<SaleDto> GetAsync(int saleId, CancellationToken cancellationToken);

        Task<int> CreateAsync(SaleDto sale, CancellationToken cancellationToken);

        Task UpdateAsync(int saleId, SaleDto sale, CancellationToken cancellationToken);

        Task DeleteManyAsync(int[] saleIds, CancellationToken cancellationToken);

        Task ImportAsync(IEnumerable<SaleDto> sales, CancellationToken cancellationToken);
    }
}
