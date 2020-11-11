using Linnworks.Core.Application.Common.Models;
using Linnworks.Core.Application.Models;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Linnworks.Web.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountriesService _countriesService;
        private readonly IRegionsService _regionsService;

        public CountriesController(
            ICountriesService countriesService,
            IRegionsService regionsService)
        {
            _countriesService = countriesService;
            _regionsService = regionsService;
        }

        [HttpGet("regions")]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetRegionsAsync()
        {
            return Ok(await _regionsService.SearchAsync(HttpContext.RequestAborted));
        }

        [HttpGet("autocomplete")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountriesByQueryAsync([FromQuery] AutocompleteCriteria autocompleteCriteria)
        {
            return Ok(await _countriesService.AutocompleteAsync(
                autocompleteCriteria,
                HttpContext.RequestAborted));
        }
    }
}
