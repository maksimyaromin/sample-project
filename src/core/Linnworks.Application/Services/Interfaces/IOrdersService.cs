using Linnworks.Core.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Services.Interfaces
{
    public interface IOrdersService
    {
        Task UpdateAsync(int orderId, OrderDto order, CancellationToken cancellationToken);
    }
}
