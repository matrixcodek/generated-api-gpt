
namespace api_gpt.DTOs
{
  public class CountryDto
  {
    public string Name { get; set; }
    public int Population { get; set; }

    public CountryDto(string name, int? population)
    {
      Name = name;
      Population = population ?? 0;
    }
  }
}