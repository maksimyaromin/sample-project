using CsvHelper;
using Linnworks.Core.Domain.Entities;
using Linnworks.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Linnworks.DataSeederUtility
{
    class Program
    {
        private static LinnworksDbContext _dbContext;
        private static int _chunkSize = 100000;
        private static int _currentChunkNo = 0;
        private static ArrayPool<Sale> _arrayPool = ArrayPool<Sale>.Shared;

        private static HashSet<string> regionKeys = new HashSet<string>();
        private static HashSet<string> countriesKeys = new HashSet<string>();
        private static HashSet<string> itemKeys = new HashSet<string>();
        private static HashSet<string> orderPriorityKeys = new HashSet<string>();
        private static HashSet<int> orderKeys = new HashSet<int>();

        private static IList<Region> regions = new List<Region>();
        private static IList<Country> countries = new List<Country>();
        private static IList<Item> items = new List<Item>();
        private static IList<OrderPriority> orderPriorities = new List<OrderPriority>();
        private static IList<Order> orders = new List<Order>();
        private static Sale[] sales = _arrayPool.Rent(_chunkSize);

        static void Startup()
        {
            var linnworksDbContextFactory = new LinnworksDbContextFactory();
            _dbContext = linnworksDbContextFactory.CreateDbContext(null);
        }

        static async Task Main(string[] args)
        {
            Startup();
            await SeedData();
            _dbContext.Dispose();
        }

        static async Task SeedData()
        {
            using var reader = new StreamReader("sales.csv");
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            csvReader.Read();
            csvReader.ReadHeader();

            try
            {
                if (_dbContext.Database.IsSqlite())
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Sales;");
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Orders;");
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Items;");
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Countries;");
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Regions;");
                    await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM OrderPriorities;");

                    _dbContext.Database.Migrate();
                }

                int index = 0;
                while (csvReader.Read())
                {
                    await SaveRecordFromCsv(csvReader, index);

                    index = index + 1 == _chunkSize
                        ? 0
                        : index + 1;
                }

                if (sales.Any(sale => sale != null))
                {
                    _dbContext.Sales.AddRange(sales.Where(sale => sale != null));
                    await _dbContext.SaveChangesAsync();

                    _arrayPool.Return(sales);

                    Console.WriteLine($"Chunk {_currentChunkNo + 1} was saved.");

                    _currentChunkNo += 1;
                }

                Console.WriteLine("All sales were parsed.");
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while migration or seeding the database.");
                throw;
            }
        }

        static async Task SaveRecordFromCsv(CsvReader csvReader, int index)
        {
            var regionKey = csvReader.GetField("Region");
            Region region;

            if (regionKeys.Contains(regionKey))
            {
                region = regions.First(region => region.Name == regionKey);
            }
            else
            {
                regionKeys.Add(regionKey);
                region = new Region { Name = regionKey };
                regions.Add(region);
            }

            var countryKey = csvReader.GetField("Country");
            Country country;

            if (countriesKeys.Contains(countryKey))
            {
                country = countries.First(country => country.Name == countryKey);
                country.Region = region;
            }
            else
            {
                countriesKeys.Add(countryKey);
                country = new Country
                {
                    Name = countryKey,
                    Region = region
                };
                countries.Add(country);
            }

            var itemKey = csvReader.GetField("Item Type");
            Item item;

            if (itemKeys.Contains(itemKey))
            {
                item = items.First(item => item.Name == itemKey);
            }
            else
            {
                itemKeys.Add(itemKey);
                item = new Item { Name = itemKey };
                items.Add(item);
            }

            var orderPriorityKey = csvReader.GetField("Order Priority");
            OrderPriority orderPriority;

            if (orderPriorityKeys.Contains(orderPriorityKey))
            {
                orderPriority = orderPriorities.First(orderPriority => orderPriority.Symbol == orderPriorityKey);
            }
            else
            {
                orderPriorityKeys.Add(orderPriorityKey);
                orderPriority = new OrderPriority { Symbol = orderPriorityKey };
                orderPriorities.Add(orderPriority);
            }

            var orderKey = csvReader.GetField<int>("Order ID");
            Order order;

            if (orderKeys.Contains(orderKey))
            {
                order = orders.First(order => order.Id == orderKey);
            }
            else
            {
                orderKeys.Add(orderKey);
                order = new Order
                {
                    Id = orderKey,
                    OrderedAt = csvReader.GetField<DateTime>("Order Date"),
                    OrderPriority = orderPriority
                };
                orders.Add(order);
            }

            var sale = new Sale
            {
                SalesChannel = csvReader.GetField("Sales Channel"),
                ShippedAt = csvReader.GetField<DateTime>("Ship Date"),
                UnitsSold = csvReader.GetField<int>("Units Sold"),
                UnitPrice = csvReader.GetField<decimal>("Unit Price"),
                UnitCost = csvReader.GetField<decimal>("Unit Cost"),
                TotalRevenue = csvReader.GetField<decimal>("Total Revenue"),
                TotalCost = csvReader.GetField<decimal>("Total Cost"),
                TotalProfit = csvReader.GetField<decimal>("Total Profit"),
                Order = order,
                Item = item,
                Country = country
            };

            sales[index] = sale;

            if (index == _chunkSize - 1)
            {
                _dbContext.Sales.AddRange(sales.ToList().Take(_chunkSize));
                await _dbContext.SaveChangesAsync();

                _arrayPool.Return(sales, true);
                sales = _arrayPool.Rent(_chunkSize);

                Console.WriteLine($"Chunk {_currentChunkNo + 1} was saved.");

                _currentChunkNo += 1;
            }
        }
    }
}
