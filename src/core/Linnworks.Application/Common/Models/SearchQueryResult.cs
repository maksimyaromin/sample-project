using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linnworks.Core.Application.Common.Models
{
    public class SearchQueryResult<T>
    {
        public int CurrentPage { get; private set; }
        
        public int Total { get; private set; }

        public IList<T> Items { get; private set; }

        public SearchQueryResult(
            List<T> items,
            SearchCriteria searchCriteria,
            int count)
        {
            CurrentPage = searchCriteria.CurrentPage;
            Total = (int)Math.Ceiling(count / (double)searchCriteria.PageSize);
            Items = items;
        }

        public bool HasPreviousPage => (CurrentPage > 1);
        
        public bool HasNextPage => (CurrentPage < Total);

        public static async Task<SearchQueryResult<T>> CreateAsync(
            IQueryable<T> source,
            SearchCriteria searchCriteria)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((searchCriteria.CurrentPage - 1) * searchCriteria.PageSize)
                .Take(searchCriteria.PageSize)
                .ToListAsync();
            return new SearchQueryResult<T>(items, searchCriteria, count);
        }
    }
}
