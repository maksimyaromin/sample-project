using Linnworks.Core.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services.Interfaces
{
    public interface IRegionsService
    {
        Task<IEnumerable<RegionDto>> SearchAsync(CancellationToken cancellationToken);
    }
}
