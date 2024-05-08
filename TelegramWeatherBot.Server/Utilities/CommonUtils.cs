using System.Text;

namespace TelegramWeatherBotServer.Utilities;

public static class CommonUtils
{
    private static readonly char[] EscapingSymbols =
        ['_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!'];

    public static DateTime ToDateTime(string? unix)
    {
        return string.IsNullOrEmpty(unix) ? default : DateTime.UnixEpoch.AddSeconds(Convert.ToDouble(unix));
    }

    public static double ToHours(string? timezone)
    {
        return string.IsNullOrEmpty(timezone) ? default : Convert.ToInt32(timezone) / 3600.0;
    }

    public static string EscapeString(string? input)
    {
        if(string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        
        var escapedString = new StringBuilder();

        foreach (var c in input)
        {
            if (Array.IndexOf(EscapingSymbols, c) >= 0)
            {
                escapedString.Append('\\');
            }

            escapedString.Append(c);
        }

        return escapedString.ToString();
    }
}