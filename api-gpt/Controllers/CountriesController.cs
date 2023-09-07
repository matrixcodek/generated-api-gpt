using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CountriesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllCountries(string? param1 = null, int? param2 = null, string? param3 = null, string? param4 = null)
    {
        var URL = _configuration.GetValue<string>("RestCountriesURL");
        Console.WriteLine($"Vamos a ver la URL: {URL}");
        var response = await _httpClient.GetAsync(URL);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return BadRequest($"Error: {response.ReasonPhrase}");
    }
}