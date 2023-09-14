using api_gpt.DTOs;
using api_gpt.Models;
using api_gpt.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text.Json;

namespace api_gpt.Tests.Services
{
    [TestClass()]
    public class CountryServiceTests
    {

        private Mock<IRequestHttpService> _mockRequestHttpService;
        private IConfiguration _configuration;
        private CountryService _countryService;

        public CountryServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                {"RestCountriesURL", "https://fakeapi.com"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(initialData: inMemorySettings)
                .Build();

            _mockRequestHttpService = new Mock<IRequestHttpService>();

            _countryService = new CountryService(_mockRequestHttpService.Object, _configuration);
        }


        [TestMethod()]
        public async Task GetAllCountries_WithNoCountriesInStore_ShouldRequestCountries()
        {
            // Arrange
            var fakeCountryName = "FakeCountry";
            var fakeResponseList = new List<Country>()
            {
                new()
                {
                    Name = new Name() { Common = fakeCountryName },
                    Population = 10_000_000
                }
            };
            
            var fakeResponse = JsonSerializer.Serialize(fakeResponseList);
            _mockRequestHttpService.Setup(service => service.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(fakeResponse);
            

            var queryParameters = new QueryParameters
            {
                CountryName = fakeCountryName,
                Population = 20,
                SortBy = null,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _countryService.GetAllCountries(queryParameters);

            // Assert
            Assert.AreEqual(1, result.TotalCount);
            Assert.AreEqual(fakeCountryName, result.FirstOrDefault()?.Name);
            _mockRequestHttpService?.Verify(service => service.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
