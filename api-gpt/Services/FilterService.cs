using api_gpt.DTOs;

namespace api_gpt.Services
{
    public static class FilterService
    {
        public static List<CountryDto> FilterCountriesByName(this List<CountryDto> countries,string? countryName = null)
        {
            if (countryName != null)
            {
                countries = countries.Where(c => c.Name.ToLower().Contains(countryName.ToLower())).ToList();
            }
            return countries;
        }

        public static List<CountryDto> FilterCountriesByPopulation(this List<CountryDto> countries, int? population)
        {
            if(population != null) 
            {
                var populationMillions = population * 1_000_000;
                countries = countries.Where(c => c.Population < populationMillions).ToList();
            }
            return countries;
        }
    }
}
