namespace TelegramWeatherBotServer.Http;

public static class Extensions
{
    public static string ToQueryString<T>(this T parameters) where T : IEnumerable<Parameter>
    {
        return parameters.Any() ? $"?{string.Join('&', parameters)}" : string.Empty;
    }
}