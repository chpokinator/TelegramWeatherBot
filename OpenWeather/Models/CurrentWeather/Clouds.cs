using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class Clouds
{
    [JsonProperty("all")]
    public int Cloudiness { get; set; }
}