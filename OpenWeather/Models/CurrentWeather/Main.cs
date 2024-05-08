using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class Main
{
    [JsonProperty("temp")]
    public string? Temp { get; set; }

    [JsonProperty("feels_like")]
    public string? FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public string? TempMin { get; set; }

    [JsonProperty("temp_max")]
    public string? TempMax { get; set; }

    [JsonProperty("pressure")]
    public string? Pressure { get; set; }

    [JsonProperty("humidity")]
    public string? Humidity { get; set; }
}