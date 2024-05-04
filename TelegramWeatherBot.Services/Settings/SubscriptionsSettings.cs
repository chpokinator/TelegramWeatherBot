using Microsoft.Extensions.Configuration;

namespace TelegramWeatherBot.Services.Settings;

public class SubscriptionsSettings
{
    public const string KEY = "subscriptionsService";
    public int SubscriptionsSyncTimeoutInMinutes { get; set; } = 30;

    public static SubscriptionsSettings Create()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false);

        var configuration = builder.Build();
        return configuration.GetSection(KEY).Get<SubscriptionsSettings>() ?? new SubscriptionsSettings();
    }
}