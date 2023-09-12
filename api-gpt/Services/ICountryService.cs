using api_gpt.DTOs;
using api_gpt.Helpers;

namespace api_gpt.Services
{
  public interface ICountryService
  {
    Task<PagedList<CountryDto>> GetAllCountries(QueryParameters queryParameters);
  }
}