using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWeather;
using Telegram.Bot;
using TelegramWeatherBotServer.Services;
using TelegramWeatherBotServer.Services.BackgroundServices;
using TelegramWeatherBotServer.Settings;

namespace TelegramWeatherBotServer.Extensions;

public static class CoreDependencies
{
    public static IServiceCollection AddSettings(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<CommonSettings>(configuration.GetSection(CommonSettings.KEY));
        collection.Configure<OpenWeatherSettings>(configuration.GetSection(OpenWeatherSettings.Key));
        
        return collection;
    }
    
    public static IServiceCollection AddWeatherServices(this IServiceCollection collection)
    {
        collection.AddTransient<OpenWeatherService>();
        return collection;
    }

    public static IServiceCollection AddBotServices(this IServiceCollection collection, IConfiguration configuration)
    {
        var settings = configuration.GetSection(CommonSettings.KEY).Get<CommonSettings>();
        TelegramBotClient botClient = new(settings!.BotToken);

        collection.AddSingleton<ITelegramBotClient>(botClient);
        collection.AddTransient<CommandsService>();
        collection.AddSingleton<CommandsCoordinator>();
        return collection;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection collection)
    {
        collection.AddHostedService<TelegramBotUpdate>();
        return collection;
    }
}