using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class Coord
{
    [JsonProperty("lon")]
    public double Lon { get; set; }

    [JsonProperty("lat")]
    public double Lat { get; set; }
}