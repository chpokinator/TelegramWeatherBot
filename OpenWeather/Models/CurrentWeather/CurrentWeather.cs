using Newtonsoft.Json;

namespace OpenWeather.Models.CurrentWeather;

public class CurrentWeather
{
    [JsonProperty("coord")]
    public Coord? Coordinates { get; set; }

    [JsonProperty("weather")]
    public List<Weather>? Weather { get; set; }

    [JsonProperty("base")]
    public string? Base { get; set; }

    [JsonProperty("main")]
    public Main? Main { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("wind")]
    public Wind? Wind { get; set; }
    
    [JsonProperty("dt")]
    public string? DateTime { get; set; }

    [JsonProperty("clouds")]
    public Clouds? Clouds { get; set; }

    [JsonProperty("sys")]
    public Sys? Sys { get; set; }

    [JsonProperty("timezone")]
    public string? Timezone { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("cod")]
    public int Cod { get; set; }
}