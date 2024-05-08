using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class Sys
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("sunrise")]
    public string? Sunrise { get; set; }

    [JsonProperty("sunset")]
    public string? Sunset { get; set; }
}