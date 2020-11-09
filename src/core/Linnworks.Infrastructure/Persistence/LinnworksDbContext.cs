using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Domain.Common;
using Linnworks.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Linnworks.Core.Infrastructure.Persistence
{
    public class LinnworksDbContext : DbContext, ILinnworksDbContext
    {
        private IDbContextTransaction _currentTransaction;

        public LinnworksDbContext(
            DbContextOptions options) : base(options)
        {}

        public DbSet<Region> Regions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<OrderPriority> OrderPriorities { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "SYSTEM";
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = "SYSTEM";
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted)
                .ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
