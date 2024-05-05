namespace TelegramWeatherBotServer.Http;

public class Parameter(string key, object value)
{
    public string Key { get; set; } = key;
    public object Value { get; set; } = value;
    
    public override string ToString()
    {
        var value = Value.ToString() ?? string.Empty;
        return $"{Key}={Uri.EscapeDataString(value)}";
    }

    public static implicit operator List<Parameter>(Parameter parameter)
    {
        return [parameter];
    }

    public static implicit operator Dictionary<string, string>(Parameter parameter)
    {
        return new Dictionary<string, string>
        {
            {parameter.Key, parameter.Value.ToString() ?? string.Empty}
        };
    }
}