using System.Text.Json.Serialization;

namespace OpenBrewery.Models;

public class BreweryDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
