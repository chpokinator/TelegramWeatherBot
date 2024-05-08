namespace HttpHelpers;

public class Parameter(string key, object? value)
{
    public override string ToString()
    {
        var value1 = value.ToString() ?? string.Empty;
        return $"{key}={Uri.EscapeDataString(value1)}";
    }
}