using api_gpt.DTOs;
using api_gpt.Services;

namespace api_gpt.Tests.Services
{
    [TestClass]
    public class FilterServiceTests
    {

       
        private List<CountryDto> _sampleCountries = new List<CountryDto>();

        [TestInitialize]
        public void Setup()
        {
            _sampleCountries = new List<CountryDto>
            {
                new(name: "CountryA", population: 2_000_000 ),
                new(name: "CountryB", population: 8_000_000 ),
                new(name: "CountryC", population: 15_000_000)
            };
        }

        [TestMethod]
        public void FilterCountriesByName_WhenCountryNameIsNull_ShouldReturnTheSameCountries()
        {
            var result = _sampleCountries.AsQueryable().FilterCountriesByName();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("CountryA", result.First().Name);
        }


        [TestMethod]
        public void FilterCountriesByName_ValidName_ReturnsFilteredCountries()
        {
            var result = _sampleCountries.AsQueryable().FilterCountriesByName("CountryA");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("CountryA", result.First().Name);
        }

        [TestMethod]
        public void FilterCountriesByPopulation_ValidPopulation_ReturnsFilteredCountries()
        {
            var result = _sampleCountries.AsQueryable().FilterCountriesByPopulation(10); // 10 million

            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.Any(c => c.Name == "CountryC"));
        }

        [TestMethod]
        public void SortBy_ValidSortByAscend_ReturnsSortedCountries()
        {
            var result = _sampleCountries.AsQueryable().SortBy("ASCEND");

            Assert.AreEqual("CountryA", result.First().Name);
            Assert.AreEqual("CountryC", result.Last().Name);
        }

        [TestMethod]
        public void SortBy_ValidSortByDescend_ReturnsSortedCountries()
        {
            var result = _sampleCountries.AsQueryable().SortBy("DESCEND");

            Assert.AreEqual("CountryC", result.First().Name);
            Assert.AreEqual("CountryA", result.Last().Name);
        }

        [TestMethod]
        public void Paginate_ValidPagination_ReturnsPaginatedResults()
        {
            var result = _sampleCountries.AsQueryable().Paginate(1, 2);
            Assert.AreEqual(2, result.Count); // 2 items per page
            Assert.AreEqual("CountryA", result.First().Name);
        }
    }
}
