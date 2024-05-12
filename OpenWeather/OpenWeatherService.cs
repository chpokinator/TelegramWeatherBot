using HttpHelpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenWeather.Models.CurrentWeather;
using OpenWeather.Models.Locations;

namespace OpenWeather;

public class OpenWeatherService(RequestSender sender, IOptions<OpenWeatherSettings> options)
{
    private readonly OpenWeatherSettings _settings = options.Value;
    private const string BASE_URL = "https://api.openweathermap.org";

    public Task<CurrentWeather?> GetCurrentWeather(string? lon, string? lat)
    {
        var parameters = new List<Parameter>
        {
            new("lat", lat),
            new("lon", lon),
            new("units", _settings.Units),
            new("lang", _settings.Language)
        };
        
        
        var response = sender.SendRequest(CreateRequest(parameters, "/data/2.5/weather")).Result.ToJToken();
        return Task.FromResult(response?.ToModel<CurrentWeather>());
    }

    public Task<List<Location>?> GetLocations(string? query)
    {
        var parameter = new Parameter("q", query);
        var response = sender.SendRequest(CreateRequest(new List<Parameter>{parameter}, "/geo/1.0/direct")).Result.ToJToken();
        if (response is JArray array)
        {
            return Task.FromResult(array.ToModelsList<Location>());
        }

        return Task.FromResult<List<Location>?>(default);
    }

    public Task<List<Location>?> GetLocations(string lat, string lon)
    {
        var parameters = new List<Parameter>
        {
            new("lat", lat),
            new("lon", lon)
        };
        
        var response = sender.SendRequest(CreateRequest(parameters, "/geo/1.0/reverse")).Result.ToJToken();
        if (response is JArray array)
        {
            return Task.FromResult(array.ToModelsList<Location>());
        }

        return Task.FromResult<List<Location>?>(default);
    }

    public static string GetIconUrl(string? iconCode)
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