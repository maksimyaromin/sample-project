using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Common.Interfaces
{
    public interface ILinnworksDbContext
    {
        DbSet<Region> Regions { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Item> Items { get; set; }

        DbSet<OrderPriority> OrderPriorities { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Sale> Sales { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
