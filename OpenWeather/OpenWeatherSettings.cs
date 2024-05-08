namespace OpenWeather;

public class OpenWeatherSettings
{
    public static readonly string Key = "OpenWeather";
    public string? AppId { get; set; } = string.Empty;
    public string? Language { get; set; } = "ua";
    public string? Units { get; set; } = "metric";
}