using HttpHelpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenWeather.Models.CurrentWeather;
using OpenWeather.Models.Locations;

namespace OpenWeather;

public class OpenWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenWeatherSettings _settings;
    private const string BASE_URL = "https://api.openweathermap.org";
    
    public OpenWeatherService(IHttpClientFactory httpClientFactory, IOptions<OpenWeatherSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _settings = options.Value;
    }

    public CurrentWeather? GetCurrentWeather(string? lon, string? lat)
    {
        var parameters = new List<Parameter>
        {
            new("lat", lat),
            new("lon", lon),
            new("units", _settings.Units),
            new("lang", _settings.Language)
        };

        var httpClient = _httpClientFactory.CreateClient();
        var response = httpClient.SendAsync(CreateRequest(parameters, "/data/2.5/weather")).Result.ToJToken();
        return response?.ToModel<CurrentWeather>();
    }

    public List<Location>? GetLocations(string? query)
    {
        var parameter = new Parameter("q", query);
        var httpClient = _httpClientFactory.CreateClient();
        
        var response = httpClient.SendAsync(CreateRequest(new List<Parameter>{parameter}, "/geo/1.0/direct")).Result.ToJToken();
        if (response is JArray array)
        {
            return array.ToModelsList<Location>();
        }

        return default;
    }

    public string GetIconUrl(string? iconCode)
    {
        return string.IsNullOrEmpty(iconCode) ? string.Empty : $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
    }

    private HttpRequestMessage CreateRequest(IEnumerable<Parameter> parameters, string method)
    {
        return new RequestBuilder(HttpMethod.Get, $"{BASE_URL}{method}")
            .AddParameter(new Parameter("appid", _settings.AppId))
            .AddParameters(parameters)
            .Build();
    }
}