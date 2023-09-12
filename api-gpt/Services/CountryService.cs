using api_gpt.DTOs;

namespace api_gpt.Services
{
  public class CountryService : ICountryService
  {
    private readonly HttpClient _httpClient;
    
    private readonly string _restCountriesURL;
    private readonly CountriesDto _countriesDto;
    public CountryService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
      _httpClient = httpClientFactory.CreateClient();
      _restCountriesURL = configuration.GetValue<string>("RestCountriesURL") ?? "";
      _countriesDto = new CountriesDto();
    }

    public async Task<List<CountryDto>> GetAllCountries(string? countryName = null, int? population = null)
    {
      if (!_countriesDto.Countries.Any()) await RequestCountries();
      return FilterCountries(countryName, population);
    }

    private List<CountryDto> FilterCountries(string? countryName = null, int? population = null)
    {
      var countries = _countriesDto.Countries.ToList();
      return countries.FilterCountriesByName(countryName).FilterCountriesByPopulation(population);
    }

    private async Task RequestCountries()
    {
      var URL = GetURL();
      var response = await _httpClient.GetAsync(URL);

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();
        _countriesDto.SetCountriesDto(content);
      }
    }

    private string GetURL(string? fields = null)
    {
      var filterFields = fields == null ? "?fields=name,population" : "?fields=" + fields;
      var endpoint = "/all";
      var URL = _restCountriesURL + endpoint + filterFields;
      return URL;
    }

  }
}