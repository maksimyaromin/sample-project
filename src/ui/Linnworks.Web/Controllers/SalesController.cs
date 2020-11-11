using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<SearchQueryResult<SaleDto>>> SearchAsync([FromQuery] SearchCriteria searchCriteria)
        {
            return await _salesService.SearchAsync(
                searchCriteria,
                HttpContext.RequestAborted);
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
    }
}
