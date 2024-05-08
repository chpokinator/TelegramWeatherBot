using Newtonsoft.Json.Linq;

namespace OpenWeather;

public static class Converter
{
    public static T? ToModel<T>(this JToken? token) where T : class
    {
        if (token is null)
        {
            return default;
        }

        try
        {
            var model = token.ToObject<T>();
            return model;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return default;
        }
    }

    public static List<T>? ToModelsList<T>(this JArray? jArray) where T : class
    {
        if (jArray is null)
        {
            return default;
        }

        try
        {
            var models = jArray.ToObject<List<T>>();
            return models;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return default;
        }
    }
}