using System.Text.Json.Serialization;

namespace api_gpt.Models
{
  public class Country
  {
    [JsonPropertyName("name")]
    public Name Name { get; set; } = new Name();
    [JsonPropertyName("population")]
    public int Population { get; set; }
  }

  public class Name
  {
    [JsonPropertyName("common")]
    public string Common { get; set; } = string.Empty;
  }

}