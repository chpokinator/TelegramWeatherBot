using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramWeatherBotServer.Settings;

namespace TelegramWeatherBotServer.Extensions;

public static class CoreDependencies
{
    public static IServiceCollection AddMainServices(this IServiceCollection collection)
    {
        return collection;
    }

    public static IServiceCollection AddSettings(this IServiceCollection collection, string configPath)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath, optional: false)
            .Build();

        collection.Configure<CommonSettings>(configuration.GetSection(CommonSettings.KEY));
        return collection;
    }
}