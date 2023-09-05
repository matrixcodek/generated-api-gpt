using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CountriesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCountries()
    {
        var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return BadRequest($"Error: {response.ReasonPhrase}");
    }
}