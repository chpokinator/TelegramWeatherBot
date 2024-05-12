using Newtonsoft.Json.Linq;

namespace HttpHelpers;

public static class Extensions
{
    public static string ToQueryString<T>(this T parameters) where T : IEnumerable<Parameter>
    {
        return parameters.Any() ? $"?{string.Join('&', parameters)}" : string.Empty;
    }

    public static JToken? ToJToken(this HttpResponseMessage? response)
    {
        if (response is null)
        {
            return default;
        }
        
        var content = response.Content.ReadAsStringAsync().Result;
        try
        {
            return JToken.Parse(content);
        }
        catch (Exception)
        {
            return default;
        }
    }
}