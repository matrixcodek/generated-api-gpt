using api_gpt.DTOs;
using api_gpt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var countries = await _countryService.GetAllCountries(queryParameters);
            var paginationMetadata = new
            {
                totalCount = countries.Count,
                pageSize = countries.PageSize,
                currentPage = countries.CurrentPage,
                totalPages = countries.TotalPages,
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            
            return Ok(countries);
        }
        
    }

}