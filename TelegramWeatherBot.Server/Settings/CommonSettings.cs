namespace TelegramWeatherBotServer.Settings;

public class CommonSettings
{
    public const string KEY = "commonSettings";
    public string UsersManagementServiceHost { get; set; } = string.Empty;
    public string BotToken { get; set; } = string.Empty;
    public int SubscriptionsSyncTimeoutInMinutes { get; set; } = 10;
}