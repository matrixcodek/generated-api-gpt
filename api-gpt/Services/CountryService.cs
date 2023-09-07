using api_gpt.DTOs;

namespace api_gpt.Services
{
  public class CountryService : ICountryService
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    private readonly string _restCountriesURL;
    private CountriesDto _countriesDto;
    public CountryService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
      _configuration = configuration;
      _httpClient = httpClientFactory.CreateClient();
      _restCountriesURL = _configuration.GetValue<string>("RestCountriesURL") ?? "";
      _countriesDto = new CountriesDto();
    }

    public async Task<List<CountryDto>> GetAllCountries(string? countryName = null)
    {
      if (!_countriesDto.Countries.Any()) await RequestCountries();
      return FilterCountries(countryName);
    }

    private List<CountryDto> FilterCountries(string? countryName = null)
    {
      var countries = _countriesDto.Countries.ToList();
      if (countryName != null)
      {
        countries = countries.Where(c => c.Name.ToLower().Contains(countryName.ToLower())).ToList();
      }
      return countries;
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