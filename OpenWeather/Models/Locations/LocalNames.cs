using Newtonsoft.Json;

namespace OpenWeather.Models.Locations;

public class LocalNames
{
    [JsonProperty("uk")]
    public string? Uk { get; set; }

    [JsonProperty("en")]
    public string? En { get; set; }
}