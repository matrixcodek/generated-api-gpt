using api_gpt.DTOs;

namespace api_gpt.Services
{
  public interface ICountryService
  {
    Task<List<CountryDto>> GetAllCountries(string? countryName = null);
  }
}