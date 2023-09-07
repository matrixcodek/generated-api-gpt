using System.Text.Json;
using api_gpt.Models;

namespace api_gpt.DTOs
{
  public class CountriesDto
  {
    public IEnumerable<CountryDto> Countries { get; set; }

    public CountriesDto()
    {
      Countries = new List<CountryDto>();
    }

    public void SetCountriesDto(string data)
    {
      var countries = JsonSerializer.Deserialize<List<Country>>(data);
      var countriesDto = countries?.Select(c => new CountryDto(c.Name.Common, c.Population));
      Countries = countriesDto ?? new List<CountryDto>();
    }
  }
}