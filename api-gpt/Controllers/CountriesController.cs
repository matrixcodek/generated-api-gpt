using api_gpt.DTOs;
using api_gpt.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_gpt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCountries([FromQuery] QueryParameters queryParameters)
        {
            return Ok(await _countryService.GetAllCountries(queryParameters.CountryName, queryParameters.Population, queryParameters.SortBy));
        }

        // [HttpGet("")]
        // public async Task<IActionResult> GetAllCountries(string? countryName = null, int? population = null, string? sortBy = null, string? param4 = null)
        // {
        //     return Ok(await _countryService.GetAllCountries(countryName, population, sortBy));
        // }
    }

}