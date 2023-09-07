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
        public async Task<IActionResult> GetAllCountries(string? countryName = null, int? param2 = null, string? param3 = null, string? param4 = null)
        {
            return Ok(await _countryService.GetAllCountries(countryName));
        }
    }

}