using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class Wind
{
    [JsonProperty("speed")]
    public string? Speed { get; set; }

    [JsonProperty("deg")]
    public int Deg { get; set; }

    [JsonProperty("gust")]
    public double Gust { get; set; }
}