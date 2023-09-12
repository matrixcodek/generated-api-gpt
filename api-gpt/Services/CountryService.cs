using api_gpt.DTOs;
using api_gpt.Helpers;

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

    public async Task<PagedList<CountryDto>> GetAllCountries(QueryParameters queryParameters)
    {
        if (!_countriesDto.Countries.Any()) await RequestCountries();

        var countries = _countriesDto.Countries.AsQueryable();
        return countries.FilterCountriesByName(queryParameters.CountryName)
                .FilterCountriesByPopulation(queryParameters.Population)
                .SortBy(queryParameters.SortBy)
                .Paginate(queryParameters.PageNumber, queryParameters.PageSize);
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