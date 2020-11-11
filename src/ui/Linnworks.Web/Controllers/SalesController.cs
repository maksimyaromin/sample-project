using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Linnworks.Web.Controllers
{
    public class SalesController : ApiController
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> SearchAsync([FromQuery] SearchCriteria searchCriteria)
        {
            return Ok(await _salesService.SearchAsync(
                searchCriteria,
                HttpContext.RequestAborted));
        }

        [HttpGet("search-options")]
        public async Task<ActionResult<SearchOptions>> SearchOptionsAsync()
        {
            return Ok(
                await _salesService.SearchOptionsAsync(HttpContext.RequestAborted));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetAsync(int id)
        {
            return Ok(
                await _salesService.GetAsync(id, HttpContext.RequestAborted));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] SaleDto sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            await _salesService.UpdateAsync(id, sale, HttpContext.RequestAborted);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<int>> CreateAsync(SaleDto sale)
        {
            return Ok(await _salesService.CreateAsync(sale, HttpContext.RequestAborted));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int[] saleIds)
        {
            await _salesService.DeleteManyAsync(saleIds, HttpContext.RequestAborted);
            return NoContent();
        }

        [HttpPut("import")]
        public async Task<ActionResult> ImportAsync(IFormFile data)
        {
            using var stream = data.OpenReadStream();
            var sales = await JsonSerializer.DeserializeAsync<List<SaleDto>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            await _salesService.ImportAsync(sales, HttpContext.RequestAborted);

            return NoContent();
        }
    }
}
