using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linnworks.Web.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet("autocomplete")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByQuery([FromQuery] AutocompleteCriteria autocompleteCriteria)
        {
            return Ok(await _itemsService.AutocompleteAsync(
                autocompleteCriteria,
                HttpContext.RequestAborted));
        }
    }
}
