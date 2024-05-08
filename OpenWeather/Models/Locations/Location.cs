using Newtonsoft.Json;

namespace OpenWeather.Models.Locations;

public class Location
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("local_names")]
    public LocalNames? LocalNames { get; set; }

    [JsonProperty("lat")]
    public string? Lat { get; set; }

    [JsonProperty("lon")]
    public string? Lon { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }
}