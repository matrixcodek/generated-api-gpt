using api_gpt.DTOs;
using api_gpt.Enums;
using api_gpt.Helpers;

namespace api_gpt.Services
{
    public static class FilterService
    {
        public static IQueryable<CountryDto> FilterCountriesByName(this IQueryable<CountryDto> countries,string? countryName = null)
        {
            if (countryName != null)
            {
                countries = countries.Where(c => c.Name.ToLower().Contains(countryName.ToLower()));
            }
            return countries;
        }

        public static IQueryable<CountryDto> FilterCountriesByPopulation(this IQueryable<CountryDto> countries, int? population)
        {
            if(population != null) 
            {
                var populationMillions = population * 1_000_000;
                countries = countries.Where(c => c.Population < populationMillions);
            }
            return countries;
        }

        public static IQueryable<CountryDto> SortBy(this IQueryable<CountryDto> countries, string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return countries;
            
            if(Enum.TryParse(sortBy,true,out SortByEnum result))
            {
                if(result == SortByEnum.ASCEND) 
                {
                    countries = countries.OrderBy(x => x.Name);
                }
                else 
                {
                    countries = countries.OrderByDescending(x => x.Name);
                }
            }
    
            return countries;
        }

        public static PagedList<CountryDto> Paginate(this IQueryable<CountryDto> countries, int pageNumber = 1, int pageSize = QueryParameters.maxPageSize)
        {
            return PagedList<CountryDto>.Create(countries, pageNumber, pageSize);
        }
    }
}
